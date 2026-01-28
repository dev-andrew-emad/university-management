using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic;
using universitydatalayer.entities;

namespace universitydatalayer.configurations
{
    public class studentconfig : IEntityTypeConfiguration<student>
    {
        public void Configure(EntityTypeBuilder<student> builder)
        {
            builder.ToTable("students");
            builder.HasKey(s => s.id);
            builder.Property(s => s.id).ValueGeneratedOnAdd().IsRequired();

            builder.Property(s => s.name).HasColumnType("varchar(50)").IsRequired();

            builder.Property(s => s.email).HasColumnType("varchar(100)").IsRequired();

            builder.Property(s => s.age).IsRequired();

            builder.Property(s => s.createdat).IsRequired();

            builder.Property(s => s.isactive).IsRequired();

            builder.HasData(
                new student { id = 1, name = "andrew emad", email = "andrew@gmail.com", age = 29, createdat = new DateTime(2026, 1, 22),isactive=true },
                new student { id = 2, name = "dina gamil", email = "dina@gmail.com", age = 27, createdat = new DateTime(2026, 1, 22),isactive=true });
        }
    }
}
