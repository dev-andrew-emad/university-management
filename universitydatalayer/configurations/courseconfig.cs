using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using universitydatalayer.entities;

namespace universitydatalayer.configurations
{
    public class courseconfig : IEntityTypeConfiguration<course>
    {
        public void Configure(EntityTypeBuilder<course> builder)
        {
            builder.ToTable("courses");
            builder.HasKey(c => c.id);
            builder.Property(c => c.id).ValueGeneratedOnAdd().IsRequired();

            builder.Property(c => c.name).HasColumnType("varchar(100)").IsRequired();

            builder.Property(c => c.hours).IsRequired();

            builder.Property(c => c.maxstudents).IsRequired();

            builder.HasOne(c => c.instructor).WithMany(i => i.courses).HasForeignKey(c => c.instructorid).
                OnDelete(DeleteBehavior.SetNull);

            builder.HasData(
                new course { id = 1, name = "english", hours = 30, maxstudents = 50, instructorid = 1 },
                new { id = 2, name = "math", hours = 60, maxstudents = 50, instructorid = 2 });
        }
    }
}
