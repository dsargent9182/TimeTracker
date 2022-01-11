using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
	public  class HoursReport
	{
		public string CompanyId { get; set; }

		public DateTime Date { get; set; }

		public DateTime Start { get; set; }

		public DateTime End { get; set; }

		public int MinutesUsed { get; set; }

		public decimal HoursUsed { get; set; }

		public string AssignmentDescription { get; set; }

		public string EffortDescription { get; set; }

		public decimal HourlyRate { get; set; }

		public string CompanyName { get; set; }

		public decimal InvoiceTotal { get; set; }

		public Guid ContractId { get; set; }
	}
}
