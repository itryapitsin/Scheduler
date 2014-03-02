using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Models.ViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string SpecialityName { get; set; }

        public bool IsActual { get; set; }

        public int StudentsCount { get; set; }


        public GroupViewModel(GroupDataTransfer @group)
        {
            Id = @group.Id;
            Code = @group.Code;
            SpecialityName = @group.SpecialityName;

            IsActual = @group.IsActual;
       
            StudentsCount = @group.StudentsCount.HasValue ? @group.StudentsCount.Value : 0;
        }
    }
}