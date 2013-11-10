using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timetable.Site.NewDataService;

namespace Timetable.Site.Models.ViewModels
{
    public class BuildingViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public BuildingViewModel(BuildingDataTransfer building)
        {
            Id = building.Id;
            Name = building.Name;
        }

        public BuildingViewModel() { }
    }
}