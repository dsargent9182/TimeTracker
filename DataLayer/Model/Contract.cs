using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
	public class Contract
	{
		public Guid Id { get; set; }

		public string CompanyName { get; set; }

		public string CompanyUrl { get; set; }

		public string Description { get; set; }

		public decimal HourlyRate { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime? EndDate { get; set; }

		public DateTime Created { get; set; }

		public DateTime? DateM { get; set; }

		public bool IsActive { get; set; }
	}
}
