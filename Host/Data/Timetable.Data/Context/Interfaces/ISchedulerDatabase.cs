using System.Linq;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Data.Context.Interfaces
{
    public interface ISchedulerDatabase: Data.Context.Interfaces.IDatabase
    {
        IQueryable<Department> Departments { get; }

        IQueryable<Auditorium> Auditoriums { get; }

        IQueryable<Faculty> Faculties { get; }

        IQueryable<Speciality> Specialities { get; }

        IQueryable<Building> Buildings { get; }

        IQueryable<Organization> Organizations { get; }

        IQueryable<Branch> Branches { get; }

        IQueryable<Course> Courses { get; }

        IQueryable<Group> Groups { get; }

        IQueryable<Lecturer> Lecturers { get; }

        IQueryable<Schedule> Schedules { get; }

        IQueryable<TutorialType> TutorialTypes { get; }

        IQueryable<Position> Positions { get; }

        IQueryable<Time> Times { get; }

        IQueryable<Tutorial> Tutorials { get; }

        IQueryable<WeekType> WeekTypes { get; }

        IQueryable<ScheduleInfo> ScheduleInfoes { get; }

        IQueryable<AuditoriumType> AuditoriumTypes { get; }

        IQueryable<StudyYear> StudyYears { get; }

        IQueryable<ScheduleType> TimetableEntities { get; }
    }
}
