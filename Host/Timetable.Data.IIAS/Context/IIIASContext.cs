﻿using System;
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
    }
}