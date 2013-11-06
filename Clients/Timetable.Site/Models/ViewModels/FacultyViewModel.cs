﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timetable.Site.DataService;

namespace Timetable.Site.Models.ViewModels
{
    public class FacultyViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public FacultyViewModel(Faculty branch)
        {
            Id = branch.Id;
            Name = branch.Name;
        }
    }
}