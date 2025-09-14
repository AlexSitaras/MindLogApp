using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace MindLog.Models.ViewModels
{
    public class MoodEntryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mood is required")]
        public string Mood { get; set; } = null!;

        [MaxLength(500)]
        public string? Note { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public string? Prevmood { get; set; }

        public IEnumerable<SelectListItem> MoodOptions { get; set; } = new List<SelectListItem>();
    }
}
