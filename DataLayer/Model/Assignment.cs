using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
	public  class Assignment
	{
		public Guid Id { get; set; }

		public Guid ContractId { get; set; }

		public string CompanyId { get; set; }

		public string Description { get; set; }

		public int MinutesAvailable { get; set; }

		public int MinutesUsed { get; set; }

		public int MinutesRemaining { get; set; }

		public DateTime Created { get; set; }

		public DateTime? DateM { get; set; }

		public bool IsActive { get; set; }

		public string CompanyUrl { get; set; }

		public string ContractDescription { get; set; }

		public string CompanyName { get; set; }

		public decimal HourlyRate { get; set; }
	}
}
