using MindLog.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MindLog.Services.Interfaces
{
    public interface IMoodEntryService
    {
        Task<List<MoodEntryDto>> GetAllAsync();
        Task<MoodEntryDto?> GetByIdAsync(int id);
        Task<MoodEntryDto> CreateAsync(MoodEntryDto dto);
        Task<bool> UpdateAsync(MoodEntryDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
