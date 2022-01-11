using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
	public class Effort
	{
		public Guid Id { get; set; }

		public Guid AssignmentId { get; set; }

		public string AssignmentDescription { get; set; }

		public string CompanyId { get; set; }

		public string CompanyUrl { get; set; }

		public DateTime WorkDate { get; set; }
		
		public DateTime StartTime { get; set; }
		
		public DateTime EndTime { get; set; }

		public int MinutesUsed { get; set; }

		public string Description { get; set; }

		public DateTime Created { get; set; }

		public DateTime? DateM { get; set; }

		public bool IsActive { get; set; }
	}
}
