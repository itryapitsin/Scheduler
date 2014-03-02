using System;

namespace Timetable.Data.IIAS.Models
{
    public class Group
    {
        public Int64 Id { get; set; }

        public string Code { get; set; }

        public Int64 StudyTypeId { get; set; }

        public int? StudentsCount { get; set; }

        //TODO: возможно потом убрать поле
        public string SpecialityName { get; set; }
    }
}
