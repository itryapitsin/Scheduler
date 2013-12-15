namespace Timetable.Site.Infrastructure
{
    public interface ICollectionRequest
    {
        uint Offset { get; set; }

        uint Limit { get; set; }
    }
}