using System;

namespace Timetable.Sync.Toolkit.Tasks
{
    public class SyncDataTask: ITask
    {
        public DateTime CreateDate { get; set; }
        public string Id { get; set; }

        private int Progress = 0;

        public SyncDataTask()
        {
            CreateDate = DateTime.Now;
        }


        public void Execute()
        {
            throw new NotImplementedException();
        }

        public int GetProgress()
        {
            return Progress;
        }
    }
}
