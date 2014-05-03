using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Models.Scheduler
{
    
    public class BuildingDataTransfer : BaseDataTransfer
    {
        
        public string Name { get; set; }
        
        public string Address { get; set; }
        
        public string ShortName { get; set; }
        
        public string Info { get; set; }
        public BuildingDataTransfer(Building building)
        {
            if(building != null){
                Id = building.Id;
                Name = building.Name;
                Address = building.Address;
                ShortName = building.ShortName;
                Info = building.Info;
            }
        }

        public BuildingDataTransfer()
        {
        }
    }
}
