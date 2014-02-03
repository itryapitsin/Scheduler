using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Models.ViewModels
{
    public class TutorialViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public TutorialViewModel(TutorialDataTransfer tutorial)
        {
            Id = tutorial.Id;
            Name = tutorial.Name;
            ShortName = tutorial.ShortName;
        }
    }
}