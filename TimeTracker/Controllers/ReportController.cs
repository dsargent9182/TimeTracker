using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.BizLayer;
using TimeTracker.Models;

namespace TimeTracker.Controllers
{
	public class ReportController : Controller
	{
		private readonly ITimeTrackerService _timeTrackerService;

		public ReportController(ITimeTrackerService timeTrackerService)
		{
			_timeTrackerService = timeTrackerService;
		}

		// GET: ReportController/Create
		public async Task<ActionResult> Create()
		{
			var model = new ReportViewModel();
			model.ContractList = await _timeTrackerService.GetContractSelectListAsync(User);
			
			return View(model);
		}

		// POST: ReportController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(ReportViewModel m)
		{
			try
			{
				//if (ModelState.IsValid)
				//{
					MemoryStream ms = await _timeTrackerService.CreateExcelReportAsync(User, m.ContractId, m.Start, m.End);
					return File(ms, "APPLICATION/octet-stream", "HoursReport.xlsx");
				//}
				return View();
			}
			catch(Exception ex)
			{
				return View();
			}
		}
	}
}
