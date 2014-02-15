using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class CreateEditAuditoriumRequest
    {
        public string Number { get; set; }

        public string Name { get; set; }

        public string Info { get; set; }

        public int? Capacity { get; set; }

        [Required]
        public int BuildingId { get; set; }

        [Required]
        public int AuditoriumTypeId { get; set; }

        public int? AuditoriumId { get; set; }
    }
}