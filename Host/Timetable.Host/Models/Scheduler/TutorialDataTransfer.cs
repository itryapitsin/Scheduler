using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    [DataContract]
    public class TutorialDataTransfer : BaseDataTransfer
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string ShortName { get; set; }
        public TutorialDataTransfer()
        {
        }

        public TutorialDataTransfer(Tutorial tutorial)
        {
            Id = tutorial.Id;
            Name = tutorial.Name;
            ShortName = tutorial.ShortName;
        }
    }
}
