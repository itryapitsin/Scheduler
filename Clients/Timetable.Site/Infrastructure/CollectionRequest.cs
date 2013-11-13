using Timetable.Site.Models.RequestModels;

namespace Timetable.Site.Infrastructure
{
    public class CollectionRequest : ICollectionRequest
    {
        private const int DefaultOffset = 0;

        private const int DefaultLimit = 50;
        public uint Offset { get; set; }
        public uint Limit { get; set; }
        public CollectionRequest(uint? offset = null, uint? limit = null)
        {
            Offset = offset ?? DefaultOffset;
            Limit = limit ?? DefaultLimit;
        }
        public CollectionRequest()
        {
            Offset = DefaultOffset;
            Limit = DefaultLimit;
        }
    }
}