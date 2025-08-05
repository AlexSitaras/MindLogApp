using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MindLog.Data;
using MindLog.Models.DTOs;
using MindLog.Models.Entity;
using MindLog.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MindLog.Services.Implementations
{
    public class MoodEntryService : IMoodEntryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MoodEntryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MoodEntryDto>> GetAllAsync()
        {
            var entities = await _context.MoodEntries
                .AsNoTracking()
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            return _mapper.Map<List<MoodEntryDto>>(entities);
        }

        public async Task<MoodEntryDto?> GetByIdAsync(int id)
        {
            var entity = await _context.MoodEntries.FindAsync(id);
            if (entity == null) return null;
            return _mapper.Map<MoodEntryDto>(entity);
        }

        public async Task<MoodEntryDto> CreateAsync(MoodEntryDto dto)
        {
            var entity = _mapper.Map<MoodEntry>(dto);
            _context.MoodEntries.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<MoodEntryDto>(entity);
        }

        public async Task<bool> UpdateAsync(MoodEntryDto dto)
        {
            var entity = await _context.MoodEntries.FindAsync(dto.Id);
            if (entity == null) return false;

            // Ενημέρωσε τις ιδιότητες
            entity.Mood = dto.Mood;
            entity.Note = dto.Note;
            entity.Date = dto.Date;

            _context.MoodEntries.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.MoodEntries.FindAsync(id);
            if (entity == null) return false;

            _context.MoodEntries.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
