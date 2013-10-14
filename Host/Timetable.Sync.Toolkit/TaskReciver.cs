using System;
using System.Messaging;
using Timetable.Sync.Toolkit.Tasks;

namespace Timetable.Sync.Toolkit
{
    public class TaskReciver
    {
        private readonly MessageQueue _messageQueue;

        public TaskReciver()
        {
            _messageQueue = MessageQueue.Exists(@".\Private$\Timetable.Dispatcher")
                ? new MessageQueue(@".\Private$\Timetable.Dispatcher")
                : MessageQueue.Create(@".\Private$\Timetable.Dispatcher");
        }

        public T Recive<T>() where T : class, ITask
        {
            _messageQueue.Formatter = new XmlMessageFormatter(new[] { typeof(T), typeof(object) });

            var message = _messageQueue.Receive();
            if(message == null)
                throw new NullReferenceException("message");

            return message.Body as T;
        }
    }
}
