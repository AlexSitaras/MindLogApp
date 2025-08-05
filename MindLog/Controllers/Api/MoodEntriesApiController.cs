using Microsoft.AspNetCore.Mvc;
using MindLog.Models.DTOs;
using MindLog.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MindLog.Controllers.Api
{
    [Route("api/v1/moodentries")]
    [ApiController]
    public class MoodEntriesApiController : ControllerBase
    {
        private readonly IMoodEntryService _service;

        public MoodEntriesApiController(IMoodEntryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<MoodEntryDto>>> GetAll()
        {
            var entries = await _service.GetAllAsync();
            return Ok(entries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MoodEntryDto>> GetById(int id)
        {
            var entry = await _service.GetByIdAsync(id);
            if (entry == null) return NotFound();
            return Ok(entry);
        }

        [HttpPost]
        public async Task<ActionResult<MoodEntryDto>> Create(MoodEntryDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MoodEntryDto dto)
        {
            if (id != dto.Id) return BadRequest("ID mismatch");
            var updated = await _service.UpdateAsync(dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
