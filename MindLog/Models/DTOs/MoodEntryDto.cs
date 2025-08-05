using System;

namespace MindLog.Models.DTOs
{
    public class MoodEntryDto
    {
        public int Id { get; set; }
        public string Mood { get; set; } = null!;
        public string? Note { get; set; }
        public DateTime Date { get; set; }
    }
}
