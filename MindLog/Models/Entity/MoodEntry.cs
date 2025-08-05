using System;
using System.ComponentModel.DataAnnotations;

namespace MindLog.Models.Entity
{
    public class MoodEntry
    {
        public int Id { get; set; }

        [Required]
        public string Mood { get; set; } = null!;  // π.χ. Happy, Sad κλπ

        public string? Note { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
