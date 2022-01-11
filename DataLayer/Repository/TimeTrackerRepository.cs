using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataLayer.Context;
using DataLayer.Model;

namespace DataLayer.Repository
{
	public class TimeTrackerRepository : ITimeTrackerRepository
	{
		private readonly DapperContext _context;

		public TimeTrackerRepository(DapperContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<Assignment>> GetAssignmentsAsync(string userId, string companyId, Guid? id = null, Guid? contractId = null, bool isAdmin = false)
		{
			IEnumerable<Assignment> assignments = new List<Assignment>();
			using (var connection = _context.CreateConnection())
			{
				assignments = await connection.QueryAsync<Assignment>("Assignment_Get", 
					new { AspNetUsersId = userId, Id = id, ContractId = contractId, CompanyId = companyId, IsAdmin = isAdmin }, 
					commandType: System.Data.CommandType.StoredProcedure);
			}
			return assignments;
		}

		public async Task CreateAssignmentAsync(string userId, Assignment assignment, bool isAdmin = false)
		{
			await CreateOrUpdateAssignmentAsync(null,userId, assignment);
		}

		public async Task UpdateAssignmentAsync(string userId, Guid Id, Assignment assignment, bool isAdmin = false)
		{
			await CreateOrUpdateAssignmentAsync(Id, userId, assignment);
		}

		public async Task CreateOrUpdateAssignmentAsync(Guid? id, string userId, Assignment a, bool isAdmin = false)
		{
			using (var connection = _context.CreateConnection())
			{
				var resp = await connection.ExecuteAsync("Assignment_Update",
						new
						{
							Id = id,
							AspNetUsersId = userId,
							ContractId = a.ContractId,
							CompanyId = a.CompanyId,
							Description = a.Description,
							MinutesAvailable = a.MinutesAvailable,
							IsAdmin = isAdmin
						},
						commandType: System.Data.CommandType.StoredProcedure);
			}
		}
		
		public async Task<IEnumerable<Contract>> GetContractsAsync(string userId,Guid? id, bool isAdmin = false, bool? isActive = null)
		{
			IEnumerable<Contract> contracts = new List<Contract>();
			using (var connection = _context.CreateConnection())
			{
				contracts = await connection.QueryAsync<Contract>("Contract_Get", 
						new { Id = id, AspNetUsersId = userId, IsAdmin = isAdmin, IsActive = isActive }, 
						commandType: System.Data.CommandType.StoredProcedure);
			}
			return contracts;
		}

		public async Task CreateOrUpdateContractAsync(Guid? id, string userId,Contract c)
		{
			using (var connection = _context.CreateConnection())
			{
				var resp = await connection.ExecuteAsync("Contract_Update",
						new {	Id = id,
								AspNetUsersId = userId, 
								CompanyName = c.CompanyName,
								Description = c.Description, 
								StartDate = c.StartDate,
								HourlyRate = c.HourlyRate 
							},
						commandType: System.Data.CommandType.StoredProcedure);
			}
		}
		public async Task CreateContractAsync(string userId, Contract contract)
		{
			await CreateOrUpdateContractAsync(null, userId, contract);
		}

		public async Task UpdateContractAsync(string userId,Guid id, Contract contract)
		{
			await CreateOrUpdateContractAsync(id, userId, contract);
		}

		public async Task<IEnumerable<Effort>> GetEffortsAsync(string userId, Guid? id, string companyId,Guid? assignmentId = null, bool isAdmin = false, bool? isActive = null)
		{
			IEnumerable<Effort> efforts = new List<Effort>();
			using (var connection = _context.CreateConnection())
			{
				efforts = await connection.QueryAsync<Effort>("Effort_Get",
					new { Id = id, AssignmentId = assignmentId,AspNetUsersId = userId,CompanyId = companyId, IsAdmin = isAdmin, IsActive = isActive },
					commandType: System.Data.CommandType.StoredProcedure);
			}
			return efforts;
		}

		public async Task CreateEffortAsync(string userId, Effort effort, bool isAdmin = false)
		{
			await CreateOrUpdateEffortAsync(null,userId, effort, isAdmin);
		}

		public async Task UpdateEffortAsync(string userId, Guid Id, Effort effort,bool? isActive = null,bool isAdmin = false)
		{
			await CreateOrUpdateEffortAsync(Id,userId,effort,isActive,isAdmin);
		}
		public async Task CreateOrUpdateEffortAsync(Guid? id, string userId, Effort e, bool? isActive = null, bool isAdmin = false)
		{
			using (var connection = _context.CreateConnection())
			{
				var resp = await connection.ExecuteAsync("Effort_Update",
						new
						{
							Id = id,
							AssignmentId = e.AssignmentId,
							AspNetUsersId = userId,
							Date = e.WorkDate,
							Description = e.Description,
							StartTime = e.StartTime,
							EndTime = e.EndTime,
							IsActive = isActive,
							IsAdmin = isAdmin
						},
						commandType: System.Data.CommandType.StoredProcedure);
			}
		}
		public async Task DeleteEffortAsync(Guid id, string userId,bool isActive,bool isAdmin = false)
		{
			using (var connection = _context.CreateConnection())
			{
				var resp = await connection.ExecuteAsync("Effort_Update",
						new
						{
							Id = id,
							AspNetUsersId = userId,
							IsActive = isActive,
							IsAdmin = isAdmin
						},
						commandType: System.Data.CommandType.StoredProcedure);
			}
		}

		public async Task<IEnumerable<HoursReport>> GetHoursReportAsync(string userId, Guid contractId, DateTime? start = null, DateTime? end = null, int? daysToRun = null, bool? isMonth = null)
		{
			IEnumerable<HoursReport> hoursReport = new List<HoursReport>();
			using (var connection = _context.CreateConnection())
			{
				hoursReport = await connection.QueryAsync<HoursReport>("Report_Hours",
					new
					{
						ContractId = contractId,
						AspNetUsersId = userId,
						dtStart = start,
						dtEnd = end,
						DaysToRun = daysToRun,
						isMonth = isMonth
					}
					,commandType: System.Data.CommandType.StoredProcedure);
			}
			return hoursReport;
		}
		public async Task<DataTable> GetHoursReportDTAsync(string userId, Guid contractId, DateTime? start = null, DateTime? end = null, int? daysToRun = null, bool? isMonth = null)
		{
			DataTable dt = new DataTable();
			using (var connection = _context.CreateConnection())
			{
				IDataReader reader = await connection.ExecuteReaderAsync("Report_Hours",
					new
					{
						ContractId = contractId,
						AspNetUsersId = userId,
						dtStart = start,
						dtEnd = end,
						DaysToRun = daysToRun,
						isMonth = isMonth
					}
					,commandType: CommandType.StoredProcedure);
				dt.Load(reader);
			}
			return dt;
		}
	}
}
