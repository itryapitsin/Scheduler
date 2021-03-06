﻿using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Models.Scheduler
{
    
    public class FacultyDataTransfer : BaseDataTransfer
    {
        
        public string Name { get; set; }
        
        public string ShortName { get; set; }
        
        public int BranchId { get; set; }
        public FacultyDataTransfer()
        {
        }

        public FacultyDataTransfer(Faculty faculty)
        {
            Id = faculty.Id;
            Name = faculty.Name;
            ShortName = faculty.ShortName;
        }
    }
}
