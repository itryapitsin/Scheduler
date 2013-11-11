using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Timetable.Site.NewDataService;
using Group = Timetable.Site.DataService.Group;

namespace Timetable.Site.Models.Groups
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public GroupViewModel(Group @group)
        {
            Id = @group.Id;
            Code = @group.Code;
        }

        public GroupViewModel(GroupDataTransfer @group)
        {
            Id = @group.Id;
            Code = @group.Code;
        }
    }
}