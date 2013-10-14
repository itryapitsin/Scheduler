using System;
using System.Messaging;
using Timetable.Dispatcher.Tasks;

namespace Timetable.Dispatcher
{
    public class TaskReciver
    {
        private MessageQueue _messageQueue;

        private void CreateQueue(string taskName)
        {
            var path = TaskDispatcherSettings.GetTaskPath(taskName);
            _messageQueue = MessageQueue.Exists(path)
                ? new MessageQueue(path)
                : MessageQueue.Create(path);
        }

        public T Recive<T>() where T : class, ITask
        {
            CreateQueue(typeof(T).Name);

            _messageQueue.Formatter = new XmlMessageFormatter(new[] { typeof(T), typeof(object) });

            var message = _messageQueue.Receive();
            if(message == null)
                throw new NullReferenceException("message");

            return message.Body as T;
        }
    }
}
