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
    public class instructorconfig : IEntityTypeConfiguration<instructor>
    {
        public void Configure(EntityTypeBuilder<instructor> builder)
        {
            builder.ToTable("instructors");
            builder.HasKey(i => i.id);
            builder.Property(i => i.id).ValueGeneratedOnAdd().IsRequired();

            builder.Property(i => i.name).HasColumnType("varchar(50)").IsRequired();

            builder.Property(i => i.email).HasColumnType("varchar(100)").IsRequired();

            builder.HasData(
                new instructor { id = 1, name = "ahmed hossam", email = "ahmed@gamil.com" },
                new instructor { id = 2, name = "youssef saad", email = "youssef@gmail.com" });
        }
    }
}
