using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TimeTracker.Models.Dto
{
	public class EffortDto
	{
		public Guid Id { get; set; }

		public Guid AssignmentId { get; set; }

		[Display(Name ="Assignment")]
		public string AssignmentDescription { get; set; }

		[Display(Name = "Assignment Description")]
		public List<SelectListItem> AssignmentSelectList { get; set; }

		public string CompanyId { get; set; }

		public string CompanyUrl { get; set; }

		[BindProperty, DataType(DataType.Date)]
		//[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
		[Display(Name = "Date")]
		public DateTime WorkDate { get; set; }

		[DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
		[Display(Name = "Start")]
		public DateTime StartTime { get; set; }

		[DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
		[Display(Name = "End")]
		public DateTime EndTime { get; set; }

		[Display(Name = "Minutes Used")]
		public int MinutesUsed { get; set; }

		[Display(Name = "Hours Used")]
		[DisplayFormat(DataFormatString = "{0:N2}")]
		public decimal HoursUsed 
		{ 
			get
			{
				return this.MinutesUsed / 60.0M;
			}
		}

		public string Description { get; set; }

		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime Created { get; set; }

		[Display(Name = "Modified")]
		public DateTime? DateM { get; set; }

		[Display(Name = "Active")]
		public bool IsActive { get; set; }


		public string CompanyLink
		{
			get
			{
				return @$"{this.CompanyUrl}/Portfolio/_workitems/edit/{this.CompanyId}";
			}
		}
	}
}
