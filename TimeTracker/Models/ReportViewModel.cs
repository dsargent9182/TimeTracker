using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TimeTracker.Models
{
	public class ReportViewModel
	{
		public List<SelectListItem> ContractList { get; set; }

		public Guid ContractId { get; set; }

		[BindProperty, DataType(DataType.Date)]
		public DateTime Start { get; set; } = DateTime.Now.AddDays(-7).Date;

		[BindProperty, DataType(DataType.Date)]
		public DateTime End { get; set; } = DateTime.Now.Date;
	}
}
