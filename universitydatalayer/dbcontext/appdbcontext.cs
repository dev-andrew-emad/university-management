using Microsoft.EntityFrameworkCore;
using universitydatalayer.configurations;
using universitydatalayer.entities;


namespace universitydatalayer.dbcontext
{
    public class appdbcontext : DbContext
    {
        public appdbcontext(DbContextOptions<appdbcontext> options) : base(options) { }

        public DbSet<student> students { get; set; }
        public DbSet<course> courses { get; set; }
        public DbSet<instructor> instructors { get; set; }
        public DbSet<studentcourse> studentcourses { get; set; }
        public DbSet<user> users { get; set; }
        public DbSet<permission> permissions { get; set; }
        public DbSet<userpermission>userpermissions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new studentconfig());
            modelBuilder.ApplyConfiguration(new instructorconfig());
            modelBuilder.ApplyConfiguration(new courseconfig());
            modelBuilder.ApplyConfiguration(new studentcourseconfig());
            modelBuilder.ApplyConfiguration(new userconfig());
            modelBuilder.ApplyConfiguration(new permissionconfig());
            modelBuilder.ApplyConfiguration(new userpermissionconfig());
        }

    }
}
