// <auto-generated />

using System.Data.Entity.Migrations.Infrastructure;
using System.Resources;

namespace Timetable.Data.Migrations.Schedule
{
    public sealed partial class Create_many_to_many_for_lecturers_and_departments : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(Create_many_to_many_for_lecturers_and_departments));
        
        string IMigrationMetadata.Id
        {
            get { return "201303171657069_Create_many_to_many_for_lecturers_and_departments"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
