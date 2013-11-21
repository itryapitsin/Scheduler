namespace Timetable.Site.Models.RequestModels
{
    public class AuditoriumAddRequest
    {
        public int BuildingId { get; set; }
        public int AuditoriumTypeId { get; set; }
        public string Number { get; set; }
        public int Capacity { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
    }
}