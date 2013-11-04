using System;
using System.Linq;
using System.Threading.Tasks;
using Timetable.Data.Context.Interfaces;
using Timetable.Data.IIAS.Models;

namespace Timetable.Data.IIAS.Context
{
    public interface IIIASContext: IDataContext
    {
        IQueryable<Building> GetBuildings();

        IQueryable<Organization> GetOrganizations();

        IQueryable<Branche> GetBranches();

        IQueryable<Faculty> GetFaculties();

        IQueryable<Department> GetDepartments();

        IQueryable<Lecturer> GetLecturers();

        IQueryable<Tutorial> GetTutorials();

        IQueryable<Speciality> GetSpecialities();

        IQueryable<TutorialType> GetTutorialTypes();

        IQueryable<Time> GetTimes();
    }
}