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
    public class studentcourseconfig : IEntityTypeConfiguration<studentcourse>
    {
        public void Configure(EntityTypeBuilder<studentcourse> builder)
        {
            builder.ToTable("studentcourses");
            builder.HasKey(sc => new { sc.studentid, sc.courseid });

            builder.HasOne(sc => sc.student).WithMany(s => s.studentcourses).HasForeignKey(sc => sc.studentid);

            builder.HasOne(sc => sc.course).WithMany(c => c.studentcourses).HasForeignKey(sc => sc.courseid);

            builder.Property(sc => sc.enrolldate).IsRequired();

            builder.HasData(
                new studentcourse { studentid = 1, courseid = 1, enrolldate = new DateTime(2026, 1, 22) },
                new studentcourse { studentid = 1, courseid = 2, enrolldate = new DateTime(2026, 1, 22) },
                new studentcourse { studentid = 2, courseid = 1, enrolldate = new DateTime(2026, 1, 22) },
                new studentcourse { studentid = 2, courseid = 2, enrolldate = new DateTime(2026, 1, 22) });
        }
    }
}
