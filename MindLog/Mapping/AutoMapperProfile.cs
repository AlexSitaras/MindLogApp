using AutoMapper;
using MindLog.Models.DTOs;
using MindLog.Models.Entity;
using MindLog.Models.ViewModels;

namespace MindLog.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Entity <-> DTO
            CreateMap<MoodEntry, MoodEntryDto>().ReverseMap();

            // DTO <-> ViewModel
            CreateMap<MoodEntryDto, MoodEntryViewModel>().ReverseMap();
        }
    }
}
