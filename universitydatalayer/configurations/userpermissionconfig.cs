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
    public class userpermissionconfig : IEntityTypeConfiguration<userpermission>
    {
        public void Configure(EntityTypeBuilder<userpermission> builder)
        {
            builder.ToTable("userpermissions");
            builder.HasKey(up => new { up.userid, up.permissionid });

            builder.HasOne(up => up.user).WithMany(u => u.userpermissions).HasForeignKey(up => up.userid);

            builder.HasOne(up => up.permission).WithMany(p => p.userpermissions).HasForeignKey(up => up.permissionid);

            builder.HasData(
                new userpermission { userid = 3, permissionid = 1 },
                new userpermission { userid = 3, permissionid = 2 },
                new userpermission { userid = 3, permissionid = 3 },
                new userpermission { userid = 4, permissionid = 2 },
                new userpermission { userid = 4, permissionid = 3 },
                new userpermission { userid=3,permissionid=4});

        }
    }
}
