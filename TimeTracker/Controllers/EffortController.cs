using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeTracker.BizLayer;
using TimeTracker.Models.Dto;

namespace TimeTracker.Controllers
{
	public class EffortController : Controller
	{
		private readonly ITimeTrackerService _timeTrackerService;

		public EffortController(ITimeTrackerService timeTrackerService)
		{
			_timeTrackerService = timeTrackerService;
		}
		// GET: EffortController
		public async Task<ActionResult> Index()
		{
			IEnumerable<TimeTracker.Models.Dto.EffortDto> efforts = await _timeTrackerService.GetEffortsAsync(User,null,null);
			return View(efforts);
		}

		// GET: EffortController/Details/5
		public async Task<ActionResult> Details(Guid id)
		{
			EffortDto effortDto = await _timeTrackerService.GetEffortAsync(User, id);
			return View(effortDto);
		}

		// GET: EffortController/Create
		public async Task<ActionResult> Create(Guid? id)
		{
			EffortDto dto = new EffortDto();
			IEnumerable<AssignmentDto> assignments = new List<AssignmentDto>();

			//Get available assignments

			if(id.HasValue)
			{
				dto.AssignmentId = id.Value;
				var assignment = await _timeTrackerService.GetAssignmentAsync(User, id.Value);
				dto.AssignmentSelectList = assignment.ContractAssignmentList;
				dto.AssignmentDescription = assignment.Description;
			}
			else
			{
				assignments = await _timeTrackerService.GetAssignmentsAsync(User,null);
				dto.AssignmentSelectList = assignments.First().ContractAssignmentList;
			}
				

			dto.WorkDate = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
			dto.StartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
			dto.EndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

			return View(dto);
		}

		// POST: EffortController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(EffortDto effortDto)
		{
			try
			{
				await _timeTrackerService.CreateEffortAsync(User, effortDto);
				return RedirectToAction(nameof(Index));
			}
			catch(Exception ex)
			{
				return View();
			}
		}

		// GET: EffortController/Edit/5
		public async Task<ActionResult> Edit(Guid id)
		{
			EffortDto effortDto = await _timeTrackerService.GetEffortAsync(User, id);
			return View(effortDto);
		}

		// POST: EffortController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(Guid id, EffortDto effortDto)
		{
			try
			{
				await _timeTrackerService.UpdateEffortAsync(User,id,effortDto);
				return RedirectToAction(nameof(Index));
			}
			catch(Exception ex)
			{
				return View();
			}
		}

		// GET: EffortController/Delete/5
		public async Task<ActionResult> Delete(Guid id)
		{
			EffortDto effortDto = await _timeTrackerService.GetEffortAsync(User, id);
			return View(effortDto);
		}

		// POST: EffortController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(Guid id,EffortDto effortDto)
		{
			try
			{
				await _timeTrackerService.DeleteEffortAsync(User, id, false);
				return RedirectToAction(nameof(Index));
			}
			catch(Exception ex)
			{
				return View();
			}
		}
	}
}
