using AutoMapper;
using OfficeOpenXml;
using DataLayer.Model;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using TimeTracker.Models;
using TimeTracker.Models.Dto;
using System.Data;
using OfficeOpenXml.Style;

namespace TimeTracker.BizLayer
{
	public class TimeTrackerService : ITimeTrackerService
	{
		private readonly IMapper _mapper;
		private readonly ITimeTrackerRepository _timeTrackerRepository;
		private readonly IConfiguration _configuration;

		public TimeTrackerService(IMapper mapper, ITimeTrackerRepository timeTrackerRepository, IConfiguration configuration)
		{
			_mapper = mapper;
			_timeTrackerRepository = timeTrackerRepository;
			_configuration = configuration;
		}

		//Assignments
		//Get all Assignments 
		public async Task<IEnumerable<AssignmentDto>> GetAssignmentsAsync(ClaimsPrincipal user, string companyId, Guid? id = null, Guid? contractId = null, bool isAdmin = false)
		{
			IEnumerable<Assignment> assignments = await _timeTrackerRepository.GetAssignmentsAsync(GetUserId(user), companyId, id, contractId, isAdmin);

			IEnumerable<AssignmentDto> assignmentDtos = _mapper.Map<IEnumerable<AssignmentDto>>(assignments);
			List<SelectListItem> selectListItems = new List<SelectListItem>();
			foreach (var a in assignmentDtos)
			{
				string text = $"{a.CompanyName}({a.HourlyRate:C2}) -> Task {a.CompanyId} {a.Description}";
				selectListItems.Add(new SelectListItem { Text = text, Value = a.Id.ToString() });
			}
			foreach (var a in assignmentDtos)
			{
				a.ContractAssignmentList = selectListItems;
			}
			return assignmentDtos;
		}
		public async Task<AssignmentDto> GetAssignmentAsync(ClaimsPrincipal user, Guid id, bool isAdmin = false)
		{
			var assignments = await GetAssignmentsAsync(user, null, id, null, isAdmin);
			var assignment = assignments.First();
			AssignmentDto dto = _mapper.Map<AssignmentDto>(assignment);
			string text = $"{dto.CompanyName}({dto.HourlyRate:C2}) -> Task {dto.CompanyId} {dto.Description}";
			dto.ContractAssignmentList = new List<SelectListItem>() { new SelectListItem { Text = text, Value = dto.Id.ToString() } };

			return dto;
		}

		public async Task<AssignmentDto> UpdateAssignmentAsync(ClaimsPrincipal user, Guid id, bool isAdmin = false, bool isActive = true)
		{
			//Get assignment
			var assignments = await GetAssignmentsAsync(user, null, id, null, isAdmin);

			//Get list of Contracts
			var selectList = await GetContractSelectListAsync(user);

			var assignment = assignments.First();
			assignment.ContractList = selectList;

			return assignments.First();
		}

		public async Task UpdateAssignmentAsync(ClaimsPrincipal user, Guid id, AssignmentDto assignmentDto, bool isAdmin = false, bool isActive = true)
		{
			Assignment assignment = _mapper.Map<Assignment>(assignmentDto);
			await _timeTrackerRepository.UpdateAssignmentAsync(GetUserId(user), id, assignment, isAdmin);
		}



		public async Task CreateAssignmentAsync(ClaimsPrincipal user, AssignmentDto assignmentDto, bool isAdmin = false)
		{
			Assignment assignment = _mapper.Map<Assignment>(assignmentDto);
			await _timeTrackerRepository.CreateAssignmentAsync(GetUserId(user), assignment, isAdmin);
		}

		public async Task<AssignmentDto> CreateAssignmentAsync(ClaimsPrincipal user, bool isAdmin = false)
		{
			//Get contracts
			var selectList = await GetContractSelectListAsync(user);
			AssignmentDto assignment = new AssignmentDto();
			assignment.ContractList = selectList;
			assignment.IsActive = true;
			assignment.MinutesAvailable = 120;
			return assignment;
		}
		public async Task<List<SelectListItem>> GetContractSelectListAsync(ClaimsPrincipal user, bool isAdmin = false, bool? isActive = null)
		{
			var contracts = await GetContractsAsync(user, null, isAdmin, isActive);
			List<SelectListItem> selectList = new List<SelectListItem>();
			foreach (ContractDto c in contracts)
			{
				string text = $"{c.CompanyName} - {c.Description} ({c.HourlyRate:C2}) ";
				selectList.Add(new SelectListItem(text, $"{c.Id}"));
			}
			return selectList;
		}



		public async Task<ContractDto> GetContractAsync(ClaimsPrincipal user, Guid id, bool isAdmin = false)
		{
			var contracts = await GetContractsAsync(user, id, isAdmin);
			return contracts.First();
		}

		public async Task<IEnumerable<ContractDto>> GetContractsAsync(ClaimsPrincipal user, Guid? id = null, bool isAdmin = false, bool? isActive = null)
		{
			IEnumerable<Contract> contracts = await _timeTrackerRepository.GetContractsAsync(GetUserId(user), id, isAdmin, isActive);

			IEnumerable<ContractDto> contractDtos = _mapper.Map<IEnumerable<ContractDto>>(contracts);

			return contractDtos;
		}

		public async Task UpdateContractAsync(ClaimsPrincipal user, Guid id, ContractDto contract)
		{
			Contract c = _mapper.Map<Contract>(contract);
			await _timeTrackerRepository.UpdateContractAsync(GetUserId(user), id, c);
		}

		public async Task CreateContractAsync(ClaimsPrincipal user, ContractDto contract)
		{
			Contract c = _mapper.Map<Contract>(contract);
			await _timeTrackerRepository.CreateContractAsync(GetUserId(user), c);
		}

		public string GetUserId(ClaimsPrincipal user, bool throwOnNull = false)
		{
			if (null != user)
			{
				Claim claimValue = user.FindFirst(ClaimTypes.NameIdentifier);
				if (claimValue != null)
				{
					return claimValue.Value;
				}
				else
				{
					throw new ArgumentNullException(nameof(claimValue));
				}
			}
			if (throwOnNull)
				throw new ArgumentNullException(nameof(user));

			return null;
		}
		//EFfort
		public async Task<IEnumerable<EffortDto>> GetEffortsAsync(ClaimsPrincipal user, string companyId, Guid? assignmentId = null, bool isAdmin = false, bool? isActive = null)
		{
			var efforts = await _timeTrackerRepository.GetEffortsAsync(GetUserId(user), null, companyId, assignmentId, isAdmin, isActive);
			IEnumerable<EffortDto> effortsDto = _mapper.Map<IEnumerable<EffortDto>>(efforts);
			return effortsDto;
		}
		public async Task<EffortDto> GetEffortAsync(ClaimsPrincipal user, Guid id, bool isAdmin = false, bool? isActive = null)
		{
			var efforts = await _timeTrackerRepository.GetEffortsAsync(GetUserId(user), id, null, null, isAdmin, isActive);
			EffortDto effortsDto = _mapper.Map<EffortDto>(efforts.First());
			return effortsDto;
		}

		public async Task UpdateEffortAsync(ClaimsPrincipal user, Guid id, EffortDto effort, bool isActive = true, bool isAdmin = false)
		{
			Effort e = _mapper.Map<Effort>(effort);
			await _timeTrackerRepository.UpdateEffortAsync(GetUserId(user), id, e, isActive, isAdmin);
		}

		public async Task DeleteEffortAsync(ClaimsPrincipal user, Guid id, bool isActive = true, bool isAdmin = false)
		{
			await _timeTrackerRepository.DeleteEffortAsync(id, GetUserId(user), isActive, isAdmin);
		}

		public async Task CreateEffortAsync(ClaimsPrincipal user, EffortDto effort, bool isAdmin = false)
		{
			Effort e = _mapper.Map<Effort>(effort);
			await _timeTrackerRepository.CreateEffortAsync(GetUserId(user), e, isAdmin);
		}

		public async Task<MemoryStream> CreateExcelReportAsync(ClaimsPrincipal user, Guid contractId, DateTime? start = null, DateTime? end = null, int? daysToRun = null, bool? isMonth = null)
		{
			var data = await _timeTrackerRepository.GetHoursReportDTAsync(GetUserId(user), contractId, start, end, daysToRun, isMonth);
			return await GenerateExcelReportAsync(data,start,end);
		}
		private async Task<MemoryStream> GenerateExcelReportAsync(DataTable dt,DateTime? start = null, DateTime? end = null)
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

			decimal hourlyRate = 0M;
			string? name = null;

			if (dt.Columns.Contains("HourlyRate"))
			{
				hourlyRate = decimal.Parse($"{dt.Rows[0]["HourlyRate"]}");
				dt.Columns.Remove("HourlyRate");
			}
			if (dt.Columns.Contains("Name"))
			{
				name = $"{dt.Rows[0]["Name"]}";
				dt.Columns.Remove("Name");
			}

			string worksheetTitle = "Hours";
			if (start.HasValue && end.HasValue)
			{
				worksheetTitle = $"{start:MMM dd yy} to {end:MMM dd yy}";
			}
			

			var stream = new MemoryStream();
			using (var package = new ExcelPackage(stream))
			{
				var ws = package.Workbook.Worksheets.Add(worksheetTitle);
				string? title = null;
				if(string.IsNullOrWhiteSpace(name))
				{
					title = "Contractor";
				}
				else
				{
					title = $"{name}, Contractor";
				}
				ws.Cells["A1"].Value = title;
				ws.Cells["A1:E1"].Merge = true;
				ws.Cells["A2:E2"].Merge = true;
				ws.Cells["A3:E3"].Merge = true;
				ws.Cells["A4:E4"].Merge = true;

				//Hourly Rate
				ws.Cells["A7"].Value = "Hourly Rate ( $/hr )";
				ws.Cells["E7"].Value = hourlyRate;
				ws.Cells["E7"].Style.Numberformat.Format = "$#,##0.00";
				ws.Cells["A7:D7"].Merge = true;

				//Total Amount
				ws.Cells["A12"].Value = "Total Amount";
				ws.Cells["E12"].Formula = "((E6*E7)*24)";
				ws.Cells["E12"].Style.Numberformat.Format = "$#,##0.00";
				ws.Cells["A12:D12"].Merge = true;

				var range = ws.Cells["A17"].LoadFromDataTable(dt, true);
				range.AutoFitColumns();
				ws.Column(1).Style.Numberformat.Format = "mm-dd-yy";
				ws.Column(2).Style.Numberformat.Format = "h:mm AM/PM";
				ws.Column(3).Style.Numberformat.Format = "h:mm AM/PM";

				ws.Column(9).Style.Numberformat.Format = "$#,##0";

				int rowCount = ws.Dimension.Rows;
				//Add a formula for the value-column
				ws.Cells[$"D18:D{rowCount}"].Formula = "C18-B18";
				ws.Column(4).Style.Numberformat.Format = "[h]:mm:ss";

				//Total Hours
				ws.Cells["A6"].Value = "Hours Total";
				ws.Cells["E6"].Formula = $"sum(D18:D{rowCount})";
				ws.Cells["E6"].Style.Numberformat.Format = "[h]:mm:ss";
				ws.Cells["A6:D6"].Merge = true;

				ws.Column(1).Width = 13.71;
				ws.Column(2).Width = 13.71;
				ws.Column(3).Width = 13.71;
				ws.Column(4).Width = 13.71;

				ws.Column(5).Width = 168;
		
				await package.SaveAsync();
			}

			stream.Position = 0;
			return stream;

		}
	}
}
