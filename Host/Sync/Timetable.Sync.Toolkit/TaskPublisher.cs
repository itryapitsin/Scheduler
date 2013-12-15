using System.Messaging;
using Timetable.Dispatcher.Tasks;

namespace Timetable.Dispatcher
{
    public class TaskPublisher
    {
        private MessageQueue _messageQueue;

        private void CreateQueue(string taskName)
        {
            var path = TaskDispatcherSettings.GetTaskPath(taskName);
            _messageQueue = MessageQueue.Exists(path)
                ? new MessageQueue(path)
                : MessageQueue.Create(path);
        }

        public void Publish(ITask task)
        {
            CreateQueue(task.GetType().Name);

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
