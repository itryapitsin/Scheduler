using System;
using System.Messaging;
using Timetable.Sync.Toolkit.Tasks;

namespace Timetable.Sync.Toolkit
{
    public class TaskPublisher
    {
        private MessageQueue _messageQueue;

        public void Publish(ITask task)
        {
            _messageQueue = MessageQueue.Exists(@".\Private$\Timetable.Dispatcher")
                ? new MessageQueue(@".\Private$\Timetable.Dispatcher")
                : MessageQueue.Create(@".\Private$\Timetable.Dispatcher");

            _messageQueue.Formatter = new XmlMessageFormatter(new[] { task.GetType(), typeof(object) });

            using (var message = new Message())
            {
                message.Body = task;
                message.Label = task.CreateDate.Ticks.ToString();
                _messageQueue.Send(message);

                task.Id = message.Id;
            }
        }
    }
}
