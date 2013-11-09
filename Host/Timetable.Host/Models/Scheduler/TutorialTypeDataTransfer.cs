using System.Runtime.Serialization;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    [DataContract]
    public class TutorialTypeDataTransfer : BaseDataTransfer
    {
        [DataMember]
        public string Name { get; set; }

        public TutorialTypeDataTransfer()
        {
        }

        public TutorialTypeDataTransfer(TutorialType tutorialType)
        {
            Id = tutorialType.Id;
            Name = tutorialType.Name;
        }
    }
}
