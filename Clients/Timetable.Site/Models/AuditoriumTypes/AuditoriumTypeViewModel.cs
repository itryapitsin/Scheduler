using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Timetable.Site.NewDataService;
using AuditoriumType = Timetable.Site.DataService.AuditoriumType;

namespace Timetable.Site.Models.AuditoriumTypes
{
    public class AuditoriumTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AuditoriumTypeViewModel(AuditoriumType auditoriumType)
        {
            Id = auditoriumType.Id;
            Name = auditoriumType.Name;
        }

        public AuditoriumTypeViewModel(AuditoriumTypeDataTransfer auditoriumType)
        {
            Id = auditoriumType.Id;
            Name = auditoriumType.Name;
        }
    }
}