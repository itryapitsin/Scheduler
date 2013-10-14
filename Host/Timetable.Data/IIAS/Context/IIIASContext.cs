using System.Linq;
using Timetable.Data.IIAS.Models;

namespace Timetable.Data.IIAS.Context
{
    public interface IIIASContext
    {
        IQueryable<Building> GetBuildings();
    }
}