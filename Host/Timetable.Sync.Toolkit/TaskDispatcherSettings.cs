using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Dispatcher.Tasks;

namespace Timetable.Dispatcher
{
    public class TaskDispatcherSettings
    {
        private const string MessageQueuePath = @".\Private$\Timetable.Dispatcher.{0}";

        public static string GetTaskPath(string taskName)
        {
            return String.Format(MessageQueuePath, taskName);
        }
    }
}
