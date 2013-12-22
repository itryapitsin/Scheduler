namespace Timetable.Site.Areas.Dispatcher.Models.ResponseModels
{
    public class FailResponse
    {
        public bool Fail { get; set; }
        public string Message { get; set; }

        public FailResponse(string message)
        {
            Fail = true;
            Message = message;
        }
    }
}