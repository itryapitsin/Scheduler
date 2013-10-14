using System;
using System.Messaging;
using Timetable.Sync.Toolkit.Tasks;

namespace Timetable.Sync.Toolkit
{
    public class TaskReciver
    {
        private MessageQueue _objMessageQueue;

        public T Recive<T>() where T : class, ITask
        {
            _objMessageQueue = MessageQueue.Exists(@".\Private$\Timetable.Dispatcher")
                ? new MessageQueue(@".\Private$\Timetable.Dispatcher")
                : MessageQueue.Create(@".\Private$\Timetable.Dispatcher");

            _objMessageQueue.Formatter = new XmlMessageFormatter(new[] { typeof(T), typeof(object) });

            var objMqMessage = _objMessageQueue.Receive();
            if(objMqMessage == null)
                throw new NullReferenceException("objMqMessage");

            return objMqMessage.Body as T;
        }
    }
}
