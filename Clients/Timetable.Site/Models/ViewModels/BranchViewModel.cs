using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timetable.Site.DataService;
using Timetable.Site.NewDataService;

namespace Timetable.Site.Models.ViewModels
{
    public class BranchViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public BranchViewModel(BranchDataTransfer branch)
        {
            Id = branch.Id;
            Name = branch.Name;
        }
    }
}