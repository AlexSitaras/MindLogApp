using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MindLog.Models.ViewModels;
using MindLog.Services.Interfaces;
using System.Threading.Tasks;

namespace MindLog.Controllers.Web
{
    public class MoodIndexController : Controller
    {
        private readonly IMoodEntryService _service;
        private readonly IMapper _mapper;

        public MoodIndexController(IMoodEntryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var dtos = await _service.GetAllAsync();
            var vms = _mapper.Map<List<MoodEntryViewModel>>(dtos);

            // Υπολογισμός κυρίαρχου mood
            var prevailingMood = dtos
                .GroupBy(m => m.Mood)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            // Υπολογισμός στατιστικών
            int total = dtos.Count;
            var stats = dtos
                .GroupBy(m => m.Mood)
                .Select(g => new MoodStat
                {
                    Mood = g.Key,
                    Count = g.Count(),
                    Percentage = Math.Round((double)g.Count() / total * 100, 2)
                })
                .ToList();

            var model = new MoodIndexViewModel
            {
                Entries = vms,
                PrevMood = prevailingMood,
                Stats = stats
            };

            return View(model);
        }

    }
}

