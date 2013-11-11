using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Runtime.Serialization;
using Timetable.Site.DataService;
using Timetable.Site.Models.ScheduleInfoes;

namespace Timetable.Site.Controllers.Api
{
    public partial class TimeController : BaseApiController<Time>
    {
        public Time[] GetTempTimes()
        {
            var result = new List<Time>();
          
            var tTime1 = new Time();
            tTime1.Building = new Building();
            tTime1.Id = 12;
            tTime1.Building.Id = 1;
            tTime1.Start = TimeSpan.Parse("8:00");
            tTime1.End = TimeSpan.Parse("9:35");
            result.Add(tTime1);

            var tTime2 = new Time();
            tTime2.Building = new Building();
            tTime2.Id = 13;
            tTime2.Building.Id = 1;
            tTime2.Start = TimeSpan.Parse("9:45");
            tTime2.End = TimeSpan.Parse("11:20");
            result.Add(tTime2);

            var tTime3 = new Time();
            tTime3.Building = new Building();
            tTime3.Id = 14;
            tTime3.Building.Id = 1;
            tTime3.Start = TimeSpan.Parse("11:30");
            tTime3.End = TimeSpan.Parse("13:05");
            result.Add(tTime3);

            var tTime4 = new Time();
            tTime4.Building = new Building();
            tTime4.Id = 15;
            tTime4.Building.Id = 1;
            tTime4.Start = TimeSpan.Parse("13:30");
            tTime4.End = TimeSpan.Parse("15:05");
            result.Add(tTime4);

            var tTime5 = new Time();
            tTime5.Building = new Building();
            tTime5.Id = 16;
            tTime5.Building.Id = 1;
            tTime5.Start = TimeSpan.Parse("15:15");
            tTime5.End = TimeSpan.Parse("16:50");
            result.Add(tTime5);

            var tTime6 = new Time();
            tTime6.Building = new Building();
            tTime6.Id = 17;
            tTime6.Building.Id = 1;
            tTime6.Start = TimeSpan.Parse("17:00");
            tTime6.End = TimeSpan.Parse("18:35");
            result.Add(tTime6);

            var tTime7 = new Time();
            tTime7.Building = new Building();
            tTime7.Id = 33;
            tTime7.Building.Id = 1;
            tTime7.Start = TimeSpan.Parse("18:35");
            tTime7.End = TimeSpan.Parse("20:00");
            result.Add(tTime7);

            return result.ToArray();
        }
    }
   
}