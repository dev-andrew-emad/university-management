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
    public class userconfig : IEntityTypeConfiguration<user>
    {
        public void Configure(EntityTypeBuilder<user> builder)
        {
            builder.ToTable("users");
            builder.HasKey(u => u.id);
            builder.Property(u => u.id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(u => u.email).IsRequired().HasColumnType("varchar(100)");

            builder.Property(u => u.password).IsRequired().HasColumnType("varchar(50)");

            builder.Property(u => u.role).IsRequired().HasColumnType("varchar(10)");

            builder.HasData(
                new user { id = 1, email = "admin@gmail.com", password = "1234", role = "admin" },
                new user { id = 2, email = "user@gmail.com", password = "1234", role = "user" });
        }
    }
}
