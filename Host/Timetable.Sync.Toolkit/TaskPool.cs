using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using Timetable.Sync.Toolkit.Tasks;

namespace Timetable.Sync.Toolkit
{
    public class TaskPool
    {
        private readonly MessageQueue _messageQueue;

        public TaskPool()
        {
            _messageQueue = MessageQueue.Exists(@".\Private$\Timetable.Dispatcher")
                ? new MessageQueue(@".\Private$\Timetable.Dispatcher")
                : MessageQueue.Create(@".\Private$\Timetable.Dispatcher");
        }

        public T GetTaskById<T>(string id) where T : class, ITask
        {
            _messageQueue.Formatter = new XmlMessageFormatter(new[] { typeof(T), typeof(object) });

            var messages = _messageQueue.GetAllMessages();
            var task = messages.First(x => x.Id == id).Body as T;

            if (task == null)
                return null;

            task.Id = id;

            return task;
        }

        public IEnumerable<ITask> GetTasks(Type taskType)
        {
            _messageQueue.Formatter = new XmlMessageFormatter(new[] { taskType, typeof(object) });

            var messages = _messageQueue.GetAllMessages();
            return messages.Select(x => x.Body as ITask);
        }
    }
}
