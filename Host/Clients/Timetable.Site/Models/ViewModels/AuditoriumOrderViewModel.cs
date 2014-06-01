using Timetable.Logic.Models.Scheduler;


namespace Timetable.Site.Models.ViewModels
{
    public class AuditoriumOrderViewModel
    {
        public int Id { get; set; }

        public string ThreadName { get; set; }

        public string LecturerName { get; set; }

        public string TutorialName { get; set; }

        public TimeViewModel Time { get; set; }

        public int TimeId { get; set; }

        public int Pair { get; set; }

        //public AuditoriumViewModel Auditorium { get; set; }

        public string AuditoriumNumber { get; set; }

        public int AuditoriumId { get; set; }

        public bool AutoDelete { get; set; }

        public string Date { get; set; }

        public AuditoriumOrderViewModel(AuditoriumOrderDataTransfer auditoriumOrder)
        {
            Id = auditoriumOrder.Id;

            ThreadName = auditoriumOrder.ThreadName;

            LecturerName = auditoriumOrder.LecturerName;

            TutorialName = auditoriumOrder.TutorialName;

            Date = auditoriumOrder.Date.ToString("yyyy-MM-dd");
            
            Time = new TimeViewModel(auditoriumOrder.Time);

            if (auditoriumOrder.Time != null) 
            {
                TimeId = auditoriumOrder.Time.Id;
                Pair = auditoriumOrder.Time.Position;
            }

            //Auditorium = new AuditoriumViewModel(auditoriumOrder.Auditorium);

            if (auditoriumOrder.Auditorium != null)
            {
                AuditoriumNumber = auditoriumOrder.Auditorium.Number;
                AuditoriumId = auditoriumOrder.Auditorium.Id;
            }

         
              
            
        }
    }
}