using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Timetable.Site.NewDataService;
using Department = Timetable.Site.DataService.Department;

namespace Timetable.Site.Models.Departments
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DepartmentViewModel(Department department)
        {
            Id = department.Id;
            Name = department.Name;
            Name = department.ShortName;
        }

        public DepartmentViewModel(DepartmentDataTransfer department)
        {
            Id = department.Id;
            Name = department.Name;
            Name = department.ShortName;
        }
    }
}