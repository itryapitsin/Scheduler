using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Data.Models.Personalization;

namespace Timetable.Logic.Models
{
    public class DataSettings
    {
        public int? AuditoriumsSelectedBuildingId { get; set; }
        public int? TabsSelectedId { get; set; }

        public DataSettings(User user)
        {
            AuditoriumsSelectedBuildingId = user.SettingsDataEditSelectedBuildingId;
            TabsSelectedId = user.SettingsDataEditSelectedTabId;
        }
    }
}
