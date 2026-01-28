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
    public class instructordata
    {
        private readonly appdbcontext _context;
        public instructordata(appdbcontext context)
        {
            _context = context;
        }
        public async Task<List<instructor>> getallinstructors()
        {
            return await _context.instructors.Include(i => i.courses).ToListAsync();
        }
        public async Task<instructor> getinstructorbyid(int id)
        {
            return await _context.instructors.Include(i => i.courses).FirstOrDefaultAsync(i => i.id == id);
        }
        public async Task<bool>exists(int instructorid)
        {
            return await _context.instructors.AnyAsync(i=>i.id== instructorid);
        }
        public async Task<int> addnewinstructor(instructor newinstructor)
        {
            _context.instructors.Add(newinstructor);
            await _context.SaveChangesAsync();
            return newinstructor.id;
        }
        public async Task<bool> updateinstructor(instructor instructor)
        {
            var existinginstructor = await _context.instructors.FirstOrDefaultAsync(i => i.id == instructor.id);
            
                existinginstructor.name = instructor.name;
                existinginstructor.email = instructor.email;
                await _context.SaveChangesAsync();
                return true;
            
        }
        public async Task<bool> deleteinstructor(int id)
        {
            var existinginstructor = await _context.instructors.FirstOrDefaultAsync(i => i.id == id);
            
                _context.instructors.Remove(existinginstructor);
                await _context.SaveChangesAsync();
                return true;
            
           
        }
    }
}
