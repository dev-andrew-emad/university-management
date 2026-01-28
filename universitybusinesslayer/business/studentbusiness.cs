using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using universitybusinesslayer.Dtos;
using universitydatalayer.data;
using universitydatalayer.entities;

namespace universitybusinesslayer.business
{
    public class studentbusiness
    {
        private readonly studentdata _studentdata;
        public studentbusiness(studentdata studentdata)
        {
            _studentdata = studentdata;
        }
        public async Task<pagedresultdto<studentdto>> getallstudents(int pagenumber, int pagesize)
        {
            if (pagenumber <= 0)
            {
                pagenumber = 1;
            }
            if (pagesize <= 0)
            {
                pagesize = 10;
            }
            int totalcount = await _studentdata.getstudentscount();
            var studentlist = await _studentdata.getallstudents(pagenumber, pagesize);
            var studentsdtolist = new List<studentdto>();
            foreach (var student in studentlist)
            {
                studentdto studentdto = new studentdto();
                studentdto.id = student.id;
                studentdto.name = student.name;
                studentdto.email = student.email;
                studentdto.age = student.age;
                studentdto.createdat = student.createdat;
                studentdto.isactive = student.isactive;
                studentsdtolist.Add(studentdto);
            }
            return new pagedresultdto<studentdto>
            {
                pagenumber = pagenumber,
                pagesize = pagesize,
                totalcount = totalcount,
                totalpages = (int)Math.Ceiling(totalcount / (double)pagesize),
                data = studentsdtolist
            };
        }
        public async Task<pagedresultdto<studentdto>> getactivestudents(int pagenumber, int pagesize)
        {
            if (pagenumber <= 0)
            {
                pagenumber = 1;
            }
            if (pagesize <= 0)
            {
                pagesize = 10;
            }
            int totalcount = await _studentdata.getactivestudentscount();
            var studentlist = await _studentdata.getactivestudents(pagenumber, pagesize);
            var studentsdtolist = new List<studentdto>();
            foreach (var student in studentlist)
            {
                studentdto studentdto = new studentdto();
                studentdto.id = student.id;
                studentdto.name = student.name;
                studentdto.email = student.email;
                studentdto.age = student.age;
                studentdto.createdat = student.createdat;
                studentdto.isactive = student.isactive;
                studentsdtolist.Add(studentdto);
            }
            return new pagedresultdto<studentdto>
            {
                pagenumber = pagenumber,
                pagesize = pagesize,
                totalcount = totalcount,
                totalpages = (int)Math.Ceiling(totalcount / (double)pagesize),
                data = studentsdtolist
            };
        }
        public async Task<pagedresultdto<studentdto>> getdeletedstudents(int pagenumber, int pagesize)
        {
            if (pagenumber <= 0)
            {
                pagenumber = 1;
            }
            if (pagesize <= 0)
            {
                pagesize = 10;
            }
            int totalcount = await _studentdata.getdeletedstudentscount();
            var studentlist = await _studentdata.getdeletedstudents(pagenumber, pagesize);
            var studentsdtolist = new List<studentdto>();
            foreach (var student in studentlist)
            {
                studentdto studentdto = new studentdto();
                studentdto.id = student.id;
                studentdto.name = student.name;
                studentdto.email = student.email;
                studentdto.age = student.age;
                studentdto.createdat = student.createdat;
                studentdto.isactive = student.isactive;
                studentsdtolist.Add(studentdto);
            }
            return new pagedresultdto<studentdto>
            {
                pagenumber = pagenumber,
                pagesize = pagesize,
                totalcount = totalcount,
                totalpages = (int)Math.Ceiling(totalcount / (double)pagesize),
                data = studentsdtolist
            };
        }
        public async Task<studentdto> getstudentbyid(int id)
        {

            var student = await _studentdata.getstudentbyid(id);
            if (student != null)
            {
                studentdto studentdto = new studentdto();
                studentdto.id = student.id;
                studentdto.name = student.name;
                studentdto.email = student.email;
                studentdto.age = student.age;
                studentdto.createdat = student.createdat;
                studentdto.isactive = student.isactive;

                return studentdto;
            }
            else
            {
                return null;
            }
        }
        public async Task<studentdto> addnewstudent(newstudentdto newstudentdto)
        {
            var newstudent = new student
            {
                name = newstudentdto.name,
                email = newstudentdto.email,
                age = newstudentdto.age,
                createdat = newstudentdto.createdat,
                isactive = newstudentdto.isactive
            };

            int newstudentid = await _studentdata.addnewstudent(newstudent);
            if (newstudentid != 0)
            {
                studentdto studentdto = new studentdto();
                studentdto.id = newstudentid;
                studentdto.name = newstudent.name;
                studentdto.email = newstudent.email;
                studentdto.age = newstudent.age;
                studentdto.createdat = newstudent.createdat;
                studentdto.isactive = newstudent.isactive;
                return studentdto;
            }
            else
            {
                return null;
            }

        }
        public async Task<bool> updatestudent(int id, newstudentdto newstudentdto)
        {
            bool exists=await _studentdata.exists(id);
            if(!exists)
                throw new Exception("student is not found");
            
            bool result = false;
            var updatestudent = new student
            {
                id = id,
                name = newstudentdto.name,
                email = newstudentdto.email,
                age = newstudentdto.age,
                createdat = newstudentdto.createdat

            };
            result = await _studentdata.updatestudent(updatestudent);
            return result;
        }
        public async Task<bool> deletestudent(int id)
        {
            var existingstudent = await _studentdata.getstudentbyid(id);
            if (existingstudent != null)
            {
                if (existingstudent.isactive == true)
                {
                    return await _studentdata.deletestudent(id);
                }
                else
                {
                    throw new Exception("student is already deleted");
                }

            }
            else
            {
                throw new Exception("student not found");
            }
        }
        public async Task<bool> restorestudent(int id)
        {

            var existingstudent = await _studentdata.getstudentbyid(id);
            if (existingstudent != null)
            {
                if (existingstudent.isactive == false)
                {
                    return await _studentdata.restorestudent(id);
                }
                else
                {
                    throw new Exception("student is already active");
                }

            }
            else
            {
                throw new Exception("student not found");
            }
        }
        public async Task<pagedresultdto<studentwithcoursesdto>> getallstudentswithcourses(int pagenumber, int pagesize)
        {
            if (pagenumber <= 0)
            {
                pagenumber = 1;
            }
            if (pagesize <= 0)
            {
                pagesize = 10;
            }
            int totalcount = await _studentdata.getstudentscount();
            var studentlist = await _studentdata.getallstudentswithcourses(pagenumber, pagesize);
            var studentsdtolist = new List<studentwithcoursesdto>();
            foreach (var student in studentlist)
            {
                studentwithcoursesdto studentdto = new studentwithcoursesdto();
                studentdto.id = student.id;
                studentdto.name = student.name;
                studentdto.email = student.email;
                studentdto.age = student.age;
                studentdto.createdat = student.createdat;
                studentdto.isactive = student.isactive;
                studentdto.courses = student.studentcourses.Select(sc => sc.course.name).ToList();
                studentsdtolist.Add(studentdto);
            }
            return new pagedresultdto<studentwithcoursesdto>
            {
                pagenumber = pagenumber,
                pagesize = pagesize,
                totalcount = totalcount,
                totalpages = (int)Math.Ceiling(totalcount / (double)pagesize),
                data = studentsdtolist
            };
        }
    }
}
