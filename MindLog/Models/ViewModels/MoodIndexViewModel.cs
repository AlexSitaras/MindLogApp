using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace MindLog.Models.ViewModels
{
    public class MoodStat
    {
        public string Mood { get; set; } = "";
        public int Count { get; set; }
        public double Percentage { get; set; }
    }

    public class MoodIndexViewModel
    {
        public List<MoodEntryViewModel> Entries { get; set; } = new();
        public string? PrevMood { get; set; }
        public List<MoodStat> Stats { get; set; } = new();
    }

}
