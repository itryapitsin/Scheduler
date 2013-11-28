﻿using System.ComponentModel.DataAnnotations;

namespace Timetable.Site.Models.RequestModels
{
    public class LecturerScheduleRequest
    {
        [Required]
        public string LecturerQuery { get; set; }
        [Required]
        public int StudyYearId { get; set; }
        [Required]
        public int Semester { get; set; }
    }
}