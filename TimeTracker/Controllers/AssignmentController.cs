using DataLayer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.BizLayer;
using TimeTracker.Models.Dto;

namespace TimeTracker.Controllers
{
	public class AssignmentController : Controller
	{
		private readonly ITimeTrackerService _timeTrackerService;

		public AssignmentController(ITimeTrackerService timeTrackerService)
		{
			_timeTrackerService = timeTrackerService;
		}
		// GET: AssignmentController
		public async Task<ActionResult> Index()
		{
			try
			{
				IEnumerable<AssignmentDto> assignments = await _timeTrackerService.GetAssignmentsAsync(User, null, null);
				return View(assignments);
			}
			catch (Exception ex)
			{
				//Log this error;
				return View(ex);
			}
	
		}

		// GET: AssignmentController/Details/5
		public async Task<ActionResult> Details(Guid id)
		{
			AssignmentDto assignment = await _timeTrackerService.GetAssignmentAsync(User,id);
			return View(assignment);
		}

		// GET: AssignmentController/Create
		public async Task<ActionResult> Create(Guid? id)
		{
			var viewModel = await _timeTrackerService.CreateAssignmentAsync(User);

			if(id.HasValue)
			{
				var item = viewModel.ContractList.Where(x => Guid.Parse(x.Value) == id).First();
				item.Selected = true;
				viewModel.ContractId = Guid.Parse(item.Value);
			}


			return View(viewModel);
		}

		// POST: AssignmentController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind("ContractId", "CompanyId", "Description", "MinutesAvailable","IsActive")]AssignmentDto assignmentDto)
		{
			try
			{
				await _timeTrackerService.CreateAssignmentAsync(User, assignmentDto);
				return RedirectToAction(nameof(Index));
			}
			catch(Exception ex)
			{
				return View();
			}
		}

		// GET: AssignmentController/Edit/5
		public async Task<ActionResult> Edit(Guid id)
		{
			try
			{
				AssignmentDto assignment = await _timeTrackerService.UpdateAssignmentAsync(User, id);
				return View(assignment);
			}
			catch(Exception ex)
			{
				return RedirectToAction(nameof(Index));
			}
		}

		// POST: AssignmentController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(Guid id, AssignmentDto assignmentDto)
		{
			try
			{
				await _timeTrackerService.UpdateAssignmentAsync(User,id, assignmentDto);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: AssignmentController/Delete/5
		public async Task<ActionResult> Delete(int id)
		{
			return View();
		}

		// POST: AssignmentController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
