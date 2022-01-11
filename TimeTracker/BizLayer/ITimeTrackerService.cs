using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using TimeTracker.Models;
using TimeTracker.Models.Dto;

namespace TimeTracker.BizLayer
{
	public interface ITimeTrackerService
	{
		//Assignments
		Task<IEnumerable<AssignmentDto>> GetAssignmentsAsync(ClaimsPrincipal user, string companyId, Guid? id = null, Guid? contractId = null, bool isAdmin = false);
		
		Task<AssignmentDto> GetAssignmentAsync(ClaimsPrincipal user, Guid id, bool isAdmin = false);

		Task<List<SelectListItem>> GetContractSelectListAsync(ClaimsPrincipal user, bool isAdmin = false, bool? isActive = null);

		Task<AssignmentDto> CreateAssignmentAsync(ClaimsPrincipal user, bool isAdmin = false);

		Task CreateAssignmentAsync(ClaimsPrincipal user, AssignmentDto assignment, bool isAdmin = false);

		Task<AssignmentDto> UpdateAssignmentAsync(ClaimsPrincipal user, Guid id, bool isAdmin = false, bool isActive = true);

		Task UpdateAssignmentAsync(ClaimsPrincipal user,Guid id, AssignmentDto assignment, bool isAdmin = false,bool isActive = true);

		//Contracts
		Task<IEnumerable<ContractDto>> GetContractsAsync(ClaimsPrincipal user, Guid? id = null, bool isAdmin = false, bool? isActive = null);
		Task<ContractDto> GetContractAsync(ClaimsPrincipal user, Guid id, bool isAdmin = false);

		Task UpdateContractAsync(ClaimsPrincipal user,Guid id, ContractDto contract);

		Task CreateContractAsync(ClaimsPrincipal user, ContractDto contract);

		//Effort
		Task<IEnumerable<EffortDto>> GetEffortsAsync(ClaimsPrincipal user,string companyId, Guid? assignmentId = null, bool isAdmin = false, bool? isActive = null);

		Task<EffortDto> GetEffortAsync(ClaimsPrincipal user, Guid id, bool isAdmin = false, bool? isActive = null);

		Task UpdateEffortAsync(ClaimsPrincipal user, Guid id, EffortDto effort, bool isActive = true,bool isAdmin = false);

		Task DeleteEffortAsync(ClaimsPrincipal user, Guid id, bool isActive = true, bool isAdmin = false);

		Task CreateEffortAsync(ClaimsPrincipal user, EffortDto effort, bool isAdmin= false);

		//Util
		string GetUserId(ClaimsPrincipal user, bool throwOnNull = false);
		Task<MemoryStream> CreateExcelReportAsync(ClaimsPrincipal user, Guid contractId, DateTime? start = null, DateTime? end = null, int? daysToRun = null, bool? isMonth = null);
	}
}