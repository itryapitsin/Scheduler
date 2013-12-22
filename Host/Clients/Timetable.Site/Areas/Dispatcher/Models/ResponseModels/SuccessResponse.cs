namespace Timetable.Site.Areas.Dispatcher.Models.ResponseModels
{
    public class SuccessResponse
    {
        public object Content { get; set; }
        public bool Ok { get; set; }
        public SuccessResponse()
        {
            Ok = true;
        }

        public SuccessResponse(object obj)
        {
            Ok = true;
            Content = obj;
        }
    }
}