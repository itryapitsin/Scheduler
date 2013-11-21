using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Models
{
    public class SemesterViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SemesterViewModel(SemesterDataTransfer semester)
        {
            Id = semester.Id;
            Name = semester.Name;
        }
    }
}