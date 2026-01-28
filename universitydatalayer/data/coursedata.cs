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
    public class coursedata
    {
        private readonly appdbcontext _context;
        public coursedata(appdbcontext context)
        {
            _context = context;
        }
        public async Task<List<course>> getallcourses()
        {
            return await _context.courses.ToListAsync();
        }
        public async Task<course> getcoursebyid(int id)
        {
            return await _context.courses.FirstOrDefaultAsync(c => c.id == id);
        }
        public async Task<int> addnewcourse(course newcourse)
        {
            _context.courses.Add(newcourse);
            await _context.SaveChangesAsync();
            return newcourse.id;
        }
        public async Task<List<course>> getcourseswithstudentscount()
        {
            return await _context.courses.Include(c => c.studentcourses).
                ThenInclude(sc => sc.student).ToListAsync();
        }
        public async Task<bool>exists(int courseid)
        {
            return await _context.courses.AnyAsync(c=>c.id == courseid);
        }
        public async Task<bool> assigninstructor(int courseid, int instructorid)
        {
            var course = await _context.courses.FirstOrDefaultAsync(c => c.id == courseid);
           
                    course.instructorid = instructorid;
                    await _context.SaveChangesAsync();
                    return true;
             
        }
        public async Task<bool> updatecourse(course course)
        {
            var existingcourse = await _context.courses.FirstOrDefaultAsync(c => c.id == course.id);
           
                existingcourse.name = course.name;
                existingcourse.hours = course.hours;
                existingcourse.maxstudents = course.maxstudents;
                existingcourse.instructorid = course.instructorid;
                await _context.SaveChangesAsync();
                return true;
           
        }
        public async Task<bool>deletecourse(int id)
        {
            var existingcourse=await _context.courses.FirstOrDefaultAsync(c=>c.id==id);
            _context.courses.Remove(existingcourse);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
