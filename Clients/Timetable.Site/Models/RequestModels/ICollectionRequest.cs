namespace Timetable.Site.Models.RequestModels
{
    public interface ICollectionRequest
    {
        uint Offset { get; set; }

        uint Limit { get; set; }
    }
}