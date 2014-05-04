using System.Collections.Generic;
using System.Linq;
using Timetable.Logic.Interfaces;
using Timetable.Logic.Models;
using Timetable.Site.Models;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Dispatcher.Models.ViewModels
{
    public class SessionScheduleViewModel
    {
       
      
        public IEnumerable<BranchViewModel> Branches { get; set; }
        public IEnumerable<FacultyViewModel> Faculties { get; set; }
        public IEnumerable<CourseViewModel> Courses { get; set; }

        public IEnumerable<TutorialViewModel> Tutorials { get; set; }
       
       
        public int? CurrentBranchId { get; set; }
        public int? CurrentFacultyId { get; set; }
        public int? CurrentCourseId { get; set; }
    

        public SessionScheduleViewModel(
            IDataService dataService,
            UserDataTransfer userData)
           
        {
          

            Branches = dataService
                .GetBranches()
                .Select(x => new BranchViewModel(x));


            if (!userData.CreatorSettings.CurrentBranchId.HasValue)
                return;

            Courses = dataService
                .GetCources(userData.CreatorSettings.CurrentBranchId.Value)
                .OrderBy(x => x.Name)
                .Select(x => new CourseViewModel(x));
            
            Faculties = dataService
                .GetFaculties(userData.CreatorSettings.CurrentBranchId.Value)
                .Select(x => new FacultyViewModel(x));



            CurrentBranchId = userData.CreatorSettings.CurrentBranchId;
            CurrentFacultyId = userData.CreatorSettings.CurrentFacultyId;
            CurrentCourseId = userData.CreatorSettings.CurrentCourseId;

            if (!userData.CreatorSettings.CurrentFacultyId.HasValue || !userData.CreatorSettings.CurrentCourseId.HasValue)
                return;

            //Tutorials = dataService.GetTutorialsForFaculty(CurrentFacultyId.Value, CurrentCourseId.Value).Select(x => new TutorialViewModel(x));
        }
    }
    
}