﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Timetable.Data.IIAS.Interfaces;
using Timetable.Data.IIAS.Models;

namespace Timetable.Data.IIAS.Context
{
    public interface IIIASContext: IDataContext
    {
        IQueryable<Building> GetBuildings();

        IQueryable<AuditoriumType> GetAuditoriumTypes();

        IQueryable<Organization> GetOrganizations();

        IQueryable<Branche> GetBranches();

        IQueryable<Faculty> GetFaculties();

        IQueryable<Department> GetDepartments();

        IQueryable<Department> GetDepartments2();

        IQueryable<Lecturer> GetLecturers();

        IQueryable<Lecturer> GetLecturers2();

        IQueryable<Tutorial> GetTutorials();

        IQueryable<Speciality> GetSpecialities();

        IQueryable<TutorialType> GetTutorialTypes();

        IQueryable<Time> GetTimes();

        IQueryable<Auditorium> GetAuditoriums();

        IQueryable<ScheduleInfo> GetScheduleInfoes();
    }
}