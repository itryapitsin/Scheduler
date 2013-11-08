using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timetable.Site.DataService;

namespace Timetable.Site.Models.ViewModels
{
    public class StudyYearViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public StudyYearViewModel(StudyYear studyYear)
        {
            Id = studyYear.Id;
            Name = string.Format("{0}/{1}", studyYear.StartYear, studyYear.StartYear + studyYear.Length);
        }
    }
}