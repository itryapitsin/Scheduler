using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timetable.Site.DataService;
using Timetable.Site.NewDataService;

namespace Timetable.Site.Models.ViewModels
{
    public class WeekTypeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public WeekTypeViewModel(WeekTypeDataTransfer weekType)
        {
            Id = weekType.Id;
            Name = weekType.Name;
        }
    }
}