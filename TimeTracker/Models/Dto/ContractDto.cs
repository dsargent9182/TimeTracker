using System.ComponentModel.DataAnnotations;

namespace TimeTracker.Models.Dto
{
	public class ContractDto
	{
		public Guid Id { get; set; }

		[Display(Name = "Company")]
		public string CompanyName { get; set; }

		public string? CompanyUrl { get; set; }

		public string Description { get; set; }

		[Display(Name = "Hourly Rate")]
		[DisplayFormat(DataFormatString = "{0:C}" )]
		public decimal HourlyRate { get; set; }

		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
		[Display(Name = "Start Date")]
		public DateTime StartDate { get; set; }
		
		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
		[Display(Name = "End Date")]
		public DateTime? EndDate { get; set; }

		[Display(Name = "Created")]
		public DateTime Created { get; set; }

		[Display(Name = "Modified")]
		public DateTime? DateM { get; set; }

		[Display(Name = "Active")]
		public bool IsActive { get; set; }
	}
}
