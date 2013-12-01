﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Data.IIAS.Models
{
    public class Speciality
    {
        public Int64 Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Code { get; set; }

        public Int64 BranchId { get; set; }
    }
}
