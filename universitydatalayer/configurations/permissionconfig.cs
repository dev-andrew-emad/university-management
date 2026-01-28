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
    public class permissionconfig : IEntityTypeConfiguration<permission>
    {
        public void Configure(EntityTypeBuilder<permission> builder)
        {
            builder.ToTable("permissions");
            builder.HasKey(p => p.id);

            builder.Property(p => p.id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.name).IsRequired().HasColumnType("varchar(50)");

            builder.HasData(
                new permission { id = 1, name = "adduser" },
                new permission { id = 2, name = "addstudent" },
                new permission { id = 3, name = "deletestudent" },
                new permission { id=4,name="deleteuser"});
        }
    }
}
