using System.Runtime.Serialization;
using Timetable.Site.DataService;


namespace Timetable.Site.Models.Times
{
    public class SendModel
    {
        public int Id { get; set; }
        public int ViewId { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string StartEnd { get; set; }
        
        public SendModel(Time t, int Index)
        {
            Id = t.Id;
            ViewId = Index;
            Start = t.Start.ToString(@"hh\:mm");
            End = t.End.ToString(@"hh\:mm");
            StartEnd = this.Start + "-" + this.End;
        }
    }
}