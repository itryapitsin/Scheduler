using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using Timetable.Dispatcher.Tasks;

namespace Timetable.Dispatcher
{
    public class TaskPool
    {
        private MessageQueue _messageQueue;

        private void CreateQueue(string taskName)
        {
            var path = TaskDispatcherSettings.GetTaskPath(taskName);
            _messageQueue = MessageQueue.Exists(path)
                ? new MessageQueue(path)
                : MessageQueue.Create(path);
        }

        public T GetTaskById<T>(string id) where T : class, ITask
        {
            CreateQueue(typeof(T).Name);

            _messageQueue.Formatter = new XmlMessageFormatter(new[] { typeof(T), typeof(object) });

            var messages = _messageQueue.GetAllMessages();
            var task = messages.First(x => x.Id == id).Body as T;

            if (task == null)
                return null;

            task.Id = id;

            return task;
        }

        public IEnumerable<T> GetTasks<T>() where T : class, ITask
        {
            CreateQueue(typeof(T).Name);

            _messageQueue.Formatter = new XmlMessageFormatter(new[] { typeof(T), typeof(object) });

            var messages = _messageQueue.GetAllMessages();
            var tasks = new List<T>();
            foreach (var message in messages)
            {
                var task = message.Body as T;
                if(task == null)
                    continue;
                task.Id = message.Id;
                tasks.Add(task);
            }
            return tasks;
        }
    }
}
