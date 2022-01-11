using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TimeTracker.BizLayer;
using TimeTracker.Models.Dto;

namespace TimeTracker.Controllers
{
	public class ContractController : Controller
	{
		private readonly ITimeTrackerService _timeTrackerService;

		public ContractController(ITimeTrackerService timeTrackerService)
		{
			_timeTrackerService = timeTrackerService;
		}
	
		// GET: ContractController
		public async Task<ActionResult> Index()
		{
			var contracts = await _timeTrackerService.GetContractsAsync(this.User);
			return View(contracts);
		}

		// GET: ContractController/Details/5
		public async Task<ActionResult> Details(Guid id)
		{
			ContractDto contract = await _timeTrackerService.GetContractAsync(this.User,id);
			return View(contract);
		}

		// GET: ContractController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: ContractController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind("CompanyName", "Description", "HourlyRate", "StartDate")]ContractDto contractDto)
		{
			try
			{
				if (ModelState.IsValid)
				{
					await _timeTrackerService.CreateContractAsync(User, contractDto);
					return RedirectToAction(nameof(Index));
				}
				return View();
			}
			catch(Exception ex)
			{
				return View();
			}
		}

		// GET: ContractController/Edit/5
		public async Task<ActionResult> Edit(Guid id)
		{
			ContractDto contract = await _timeTrackerService.GetContractAsync(this.User, id);
			return View(contract);
		}

		// POST: ContractController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(Guid id, 
				[Bind ("CompanyName", "Description", "HourlyRate", "StartDate","EndDate", "IsActive")] ContractDto contractDto)
		{
			try
			{
				if(ModelState.IsValid)
				{
					await _timeTrackerService.UpdateContractAsync(User,id,contractDto);
					return RedirectToAction(nameof(Index));
				}
				return View();
			}
			catch(Exception ex)
			{
				return View();
			}
		}

		// GET: ContractController/Delete/5
		public async Task<ActionResult> Delete(int id)
		{
			return View();
		}

		// POST: ContractController/Delete/5
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
