using System;

namespace Timetable.Data.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool IsActual { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}