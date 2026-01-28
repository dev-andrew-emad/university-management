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
    public class studentdata
    {
        private readonly appdbcontext _context;
        public studentdata(appdbcontext context)
        {
            _context = context;
        }
        public async Task<int> getstudentscount()
        {
            return await _context.students.CountAsync();
        }
        public async Task<int> getactivestudentscount()
        {
            return await _context.students.Where(s => s.isactive == true).CountAsync();
        }
        public async Task<int> getdeletedstudentscount()
        {
            return await _context.students.Where(s => s.isactive == false).CountAsync();
        }
        public async Task<List<student>> getallstudents(int pagenumber, int pagesize)
        {
            return await _context.students.OrderBy(s => s.id).
                Skip((pagenumber - 1) * pagesize).Take(pagesize).ToListAsync();
        }
        public async Task<List<student>> getactivestudents(int pagenumber, int pagesize)
        {
            return await _context.students.OrderBy(s => s.id).Where(s => s.isactive == true).
                Skip((pagenumber - 1) * pagesize).Take(pagesize).ToListAsync();
        }
        public async Task<List<student>> getdeletedstudents(int pagenumber, int pagesize)
        {
            return await _context.students.OrderBy(s => s.id).Where(s => s.isactive == false).
                Skip((pagenumber - 1) * pagesize).Take(pagesize).ToListAsync();
        }

        public async Task<student> getstudentbyid(int id)
        {
            return await _context.students.FirstOrDefaultAsync(s => s.id == id);
        }
        public async Task<bool>exists(int id)
        {
            return await _context.students.AnyAsync(s=>s.id== id);
        }
        public async Task<int> addnewstudent(student newstudent)
        {
            _context.students.Add(newstudent);
            await _context.SaveChangesAsync();
            return newstudent.id;
        }
        public async Task<bool> updatestudent(student student)
        {
            var existingstudent = await _context.students.FirstOrDefaultAsync(s => s.id == student.id);
            
            
                existingstudent.name = student.name;
                existingstudent.email = student.email;
                existingstudent.age = student.age;
                existingstudent.createdat = student.createdat;


                await _context.SaveChangesAsync();
                return true;
           
        }
        public async Task<bool> deletestudent(int id)
        {
            var existingstudent = await _context.students.FirstOrDefaultAsync(s => s.id == id);
            
            
                existingstudent.isactive = false;
                await _context.SaveChangesAsync();
                return true;
            
        }
        public async Task<bool> restorestudent(int id)
        {

            var existingstudent = await _context.students.FirstOrDefaultAsync(s => s.id == id);
            
            
                existingstudent.isactive = true;
                await _context.SaveChangesAsync();
                return true;
         
        }
        public async Task<List<student>> getallstudentswithcourses(int pagenumber, int pagesize)
        {
            return await _context.students.Include(s => s.studentcourses).ThenInclude(sc => sc.course).
                Skip((pagenumber - 1) * pagesize).Take(pagesize).ToListAsync();
        }
    }
}
