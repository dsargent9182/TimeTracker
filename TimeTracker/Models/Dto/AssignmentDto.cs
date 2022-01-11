using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TimeTracker.Models.Dto
{
	public class AssignmentDto
	{
		public List<SelectListItem> ContractList { get; set; }

		public List<SelectListItem> ContractAssignmentList { get; set; }
		public Guid Id { get; set; }

		[Display(Name = "Contract Id")]
		public Guid ContractId { get; set; }

		[Display(Name = "Task Id")]
		public string CompanyId { get; set; }

		public string Description { get; set; }

		
		[Display(Name = "Minutes Available")]
		public int MinutesAvailable { get; set; }

		[Display(Name = "Hours Available")]
		public decimal HoursAvailable { get { return (this.MinutesAvailable / 60M); } }

		
		[Display(Name = "Minutes Used")]
		public int MinutesUsed { get; set; }

		[Display(Name = "Hours Used")]
		public decimal HoursUsed { get { return (this.MinutesUsed / 60M); } }

		public int MinutesRemaining { get; set; }

		[Display(Name = "Hours Remain")]
		public decimal HoursRemaining { get { return (this.MinutesRemaining / 60M); } }

		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime Created { get; set; }

		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime? DateM { get; set; }

		public bool IsActive { get; set; }

		public string CompanyUrl { get; set; }

		public string ContractDescription { get; set; }

		public string CompanyName { get; set; }
		
		[DisplayFormat(DataFormatString = "{0:C}")]
		public decimal HourlyRate { get; set; }

	}
}
