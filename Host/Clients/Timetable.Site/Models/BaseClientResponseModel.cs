using Timetable.Logic.Interfaces;

namespace Timetable.Site.Models
{
    public abstract class BaseClientResponseModel
    {
        protected IDataService DataService { get; private set; }
        protected BaseClientResponseModel(IDataService dataService)
        {
            DataService = dataService;
        }
    }
}