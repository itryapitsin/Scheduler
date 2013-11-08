using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timetable.Site.DataService;

namespace Timetable.Site.Models.ViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public GroupViewModel(Group group)
        {
            Id = group.Id;
            Code = group.Code;
        }
    }
}