//using DataLayer.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DataLayer.Repository
//{
//	public class DummyTimeTrackerRepo : ITimeTrackerRepository
//	{
//		public async Task<IEnumerable<Assignment>> GetAssignments(Guid? id, Guid? contractId, string companyId)
//		{
//			List<Assignment> assignments = new List<Assignment>();
//			await Task.Run(() => assignments.Add(new Assignment { Id = new Guid(), CompanyId = "Dummy123", ContractId = new Guid(),
//				Created = DateTime.Now, Description = "Dummy description", MinutesAvailable = 600 }));
//			return assignments;
//		}
//	}
//}
