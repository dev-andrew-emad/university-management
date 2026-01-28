using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using universitydatalayer.dbcontext;
using universitydatalayer.entities;

namespace universitydatalayer.data
{
    public class studentcoursesdata
    {
        private readonly appdbcontext _context;
        public studentcoursesdata(appdbcontext context)
        {
            _context = context;
        }
        public async Task<bool> exists(int studentid, int courseid)
        {
            return await _context.studentcourses.AnyAsync(sc => sc.studentid == studentid && sc.courseid == courseid);
        }
        public async Task<course> getcoursewithstudents(int courseid)
        {
            return await _context.courses.Include(c => c.studentcourses).ThenInclude(sc => sc.student).FirstOrDefaultAsync(c => c.id == courseid);
        }
        public async Task enrollstudent(studentcourse studentcourse)
        {
            _context.studentcourses.Add(studentcourse);
            await _context.SaveChangesAsync();


        }
    }
}
