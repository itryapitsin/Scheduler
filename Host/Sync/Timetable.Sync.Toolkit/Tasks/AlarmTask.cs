using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Dispatcher.Tasks
{
    public class AlarmTask: ITask
    {
        public DateTime CreateDate { get; set; }
        public string Id { get; set; }

        public void Execute()
        {
            CreateDate = DateTime.Now;
        }
    }
}
