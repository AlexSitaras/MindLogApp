using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MindLog.Models.ViewModels;
using MindLog.Services.Interfaces;
using System.Threading.Tasks;

namespace MindLog.Controllers.Web
{
    public class MoodEntriesController : Controller
    {
        private readonly IMoodEntryService _service;
        private readonly IMapper _mapper;

        public MoodEntriesController(IMoodEntryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: /MoodEntries
        public async Task<IActionResult> Index()
        {
            var dtos = await _service.GetAllAsync();
            var vms = _mapper.Map<List<MoodEntryViewModel>>(dtos);
            return View(vms);
        }

        // GET: /MoodEntries/Create
        public IActionResult Create()
        {
            return View(new MoodEntryViewModel { Date = System.DateTime.Today });
        }

        // POST: /MoodEntries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MoodEntryViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var dto = _mapper.Map<Models.DTOs.MoodEntryDto>(vm);
            await _service.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // GET: /MoodEntries/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound();

            var vm = _mapper.Map<MoodEntryViewModel>(dto);
            return View(vm);
        }

        // POST: /MoodEntries/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MoodEntryViewModel vm)
        {
            if (id != vm.Id) return BadRequest();
            if (!ModelState.IsValid) return View(vm);

            var dto = _mapper.Map<Models.DTOs.MoodEntryDto>(vm);
            var updated = await _service.UpdateAsync(dto);
            if (!updated) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // GET: /MoodEntries/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound();

            var vm = _mapper.Map<MoodEntryViewModel>(dto);
            return View(vm);
        }

        // POST: /MoodEntries/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

