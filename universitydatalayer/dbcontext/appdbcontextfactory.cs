using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace universitydatalayer.dbcontext
{
    public class appdbcontextfactory : IDesignTimeDbContextFactory<appdbcontext>
    {
        public appdbcontext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<appdbcontext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=universitymanagement;User Id=sa;Password=123456;TrustServerCertificate=True;");

            return new appdbcontext(optionsBuilder.Options);
        }
    }


}
