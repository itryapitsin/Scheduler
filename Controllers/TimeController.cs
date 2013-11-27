using System.Linq;
using System.Web.Mvc;
using Timetable.Site.Infrastructure;
using Timetable.Site.Models;

namespace Timetable.Site.Controllers
{
    public class TimeController : NewBaseController
    {
        public ActionResult Get(int buildingId)
        {
            UserData.CreatorSettings.CurrentBuildingId = buildingId;
            UserService.SaveUserState(UserData);

            var times = DataService
                .GetTimes(buildingId)
                .Select(x => new TimeViewModel(x));

            return new JsonNetResult(times);
        }
    }
}