using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Base.Entities.Scheduler;

namespace Timetable.Data.Mapping
{
    public class OrganizationMapping : EntityTypeConfiguration<Organization>
    {
        public OrganizationMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasMany(x => x.Branches)
                .WithRequired(x => x.Organization)
                .HasForeignKey(x => x.OrganizationId)
                .WillCascadeOnDelete(false);
        }
    }
}
