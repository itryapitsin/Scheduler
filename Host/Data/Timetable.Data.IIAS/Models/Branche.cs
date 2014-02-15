using System;

namespace Timetable.Data.IIAS.Models
{
    public class Branche
    {
        public Int64 Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public Int64 OrganizationId { get; set; }
    }
}
