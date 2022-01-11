using DataLayer.Model;
using System.Data;

namespace DataLayer.Repository
{
	public interface ITimeTrackerRepository
	{
		//Assignments
		Task<IEnumerable<Assignment>> GetAssignmentsAsync(string userId, string companyId, Guid? id = null, Guid? contractId = null, bool isAdmin = false);
		Task CreateAssignmentAsync(string userId, Assignment assignment, bool isAdmin = false);
		Task UpdateAssignmentAsync(string userId, Guid Id, Assignment assignment, bool isAdmin = false);
		
		//Contracts
		Task<IEnumerable<Contract>> GetContractsAsync(string userId, Guid? id, bool isAdmin = false, bool? isActive = null);
		Task CreateContractAsync(string userId, Contract contract);
		Task UpdateContractAsync(string userId, Guid Id, Contract contract);

		//Effort
		Task<IEnumerable<Effort>> GetEffortsAsync(string userId, Guid? id, string companyId, Guid? assignmentId = null, bool isAdmin = false, bool? isActive = null);
		Task CreateEffortAsync(string userId, Effort effort, bool isAdmin = false);
		Task UpdateEffortAsync(string userId, Guid Id, Effort effort,  bool? isActive = null,bool isAdmin = false);

		Task DeleteEffortAsync(Guid id, string userId, bool isActive, bool isAdmin = false);

		Task<IEnumerable<HoursReport>> GetHoursReportAsync(string userId, Guid contractId, DateTime? start = null, DateTime? end = null, int? daysToRun = null, bool? isMonth = null);

		Task<DataTable> GetHoursReportDTAsync(string userId, Guid contractId, DateTime? start = null, DateTime? end = null, int? daysToRun = null, bool? isMonth = null);

		

	}
}