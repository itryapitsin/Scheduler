﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timetable.Site.DataService;

namespace Timetable.Site.Models.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CourseViewModel(Course course)
        {
            Id = course.Id;
            Name = course.Name;
        }
    }
}