using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timetable.Site.DataService;

namespace Timetable.Site.Models.ViewModels
{
    public class SpecialityViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SpecialityViewModel(Speciality speciality)
        {
            Id = speciality.Id;
            Name = string.Format("{0} {1}", speciality.Name, speciality.Code);
        }
    }
}