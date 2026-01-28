using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using universitybusinesslayer.Dtos;
using universitydatalayer.data;
using universitydatalayer.entities;

namespace universitybusinesslayer.business
{
    public class instructorbusiness
    {
        private readonly instructordata _instructordata;
        public instructorbusiness(instructordata instructordata)
        {
            _instructordata = instructordata;
        }
        public async Task<List<instructordto>> getallinstructors()
        {
            var instructorslist = await _instructordata.getallinstructors();
            var instructorsdtolist = new List<instructordto>();
            foreach (var instructor in instructorslist)
            {
                instructordto instructordto = new instructordto();
                instructordto.id = instructor.id;
                instructordto.name = instructor.name;
                instructordto.email = instructor.email;
                instructordto.courses = instructor.courses.Select(c => c.name).ToList();
                instructorsdtolist.Add(instructordto);
            }
            return instructorsdtolist;
        }
        public async Task<instructordto> getinstructorbyid(int id)
        {
            var instructor = await _instructordata.getinstructorbyid(id);
            if (instructor != null)
            {
                instructordto instructordto = new instructordto();
                instructordto.id = instructor.id;
                instructordto.name = instructor.name;
                instructordto.email = instructor.email;
                instructordto.courses = instructor.courses.Select(c => c.name).ToList();
                return instructordto;
            }
            else
            {
                throw new Exception("instructor is not found");
            }
        }
        public async Task<instructordto> addnewinstructor(newinstructordto newinstructordto)
        {
            var instructor = new instructor
            {
                name = newinstructordto.name,
                email = newinstructordto.email
            };
            int instructorid = await _instructordata.addnewinstructor(instructor);
            if (instructorid != 0)
            {
                instructordto instructordto = new instructordto();
                instructordto.id = instructorid;
                instructordto.name = instructor.name;
                instructordto.email = instructor.email;
                return instructordto;
            }
            else
            {
                return null;
            }
        }
        public async Task<bool> updateinstructor(int id, newinstructordto newinstructordto)
        {
            bool exist=await _instructordata.exists(id);
            if (!exist)
                throw new Exception("instructor is not found");

            var instructor = new instructor
            {
                id = id,
                name = newinstructordto.name,
                email = newinstructordto.email
            };
            bool result = await _instructordata.updateinstructor(instructor);
            return result;
        }
        public async Task<bool> deleteinstructor(int id)
        {
            var existinginstructor = await _instructordata.getinstructorbyid(id);
            if (existinginstructor != null)
            {
                return await _instructordata.deleteinstructor(id);
            }
            else
            {
                throw new Exception("instructor is not found");
            }
        }
    }
}
