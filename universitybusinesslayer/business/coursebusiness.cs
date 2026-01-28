using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using universitybusinesslayer.Dtos;
using universitydatalayer.data;
using universitydatalayer.dbcontext;
using universitydatalayer.entities;

namespace universitybusinesslayer.business
{
    public class coursebusiness
    {
        private readonly coursedata _coursedata;
        private readonly instructordata _instructordata;
        public coursebusiness(coursedata coursedata,instructordata instructordata)
        {
            _coursedata = coursedata;
            _instructordata = instructordata;
        }
        public async Task<List<coursedto>> getallcourses()
        {
            var courseslist = await _coursedata.getallcourses();

            var coursesdtolist = new List<coursedto>();
            foreach (var course in courseslist)
            {
                coursedto coursedto = new coursedto();
                coursedto.id = course.id;
                coursedto.name = course.name;
                coursedto.hours = course.hours;
                coursedto.maxstudents = course.maxstudents;
                if (course.instructorid != null)
                {
                    coursedto.instructorid = course.instructorid;
                }
                else
                {
                    coursedto.instructorid = null;
                }
                coursesdtolist.Add(coursedto);

            }
            return coursesdtolist;
        }
        public async Task<coursedto> getcoursebyid(int id)
        {
            var course = await _coursedata.getcoursebyid(id);
            if (course != null)
            {
                coursedto coursedto = new coursedto();
                coursedto.id = course.id;
                coursedto.name = course.name;
                coursedto.hours = course.hours;
                coursedto.maxstudents = course.maxstudents;
                if (course.instructorid != null)
                {
                    coursedto.instructorid = course.instructorid;
                }
                else
                {
                    coursedto.instructorid = null;
                }
                return coursedto;
            }
            else
            {
                return null;
            }
        }
        public async Task<coursedto> addnewcourse(newcoursedto newcoursedto)
        {
            var newcourse = new course();
            newcourse.name = newcoursedto.name;
            newcourse.hours = newcoursedto.hours;
            newcourse.maxstudents = newcoursedto.maxstudents;
            if (newcoursedto.instructorid != null)
            {
                newcourse.instructorid = newcoursedto.instructorid;
            }
            else
            {
                newcourse.instructorid = null;
            }

            int newcourseid = await _coursedata.addnewcourse(newcourse);
            if (newcourseid != 0)
            {
                coursedto coursedto = new coursedto();
                coursedto.id = newcourseid;
                coursedto.name = newcourse.name;
                coursedto.hours = newcourse.hours;
                coursedto.maxstudents = newcourse.maxstudents;
                if (newcourse.instructorid != null)
                {
                    coursedto.instructorid = newcourse.instructorid;
                }
                else
                {
                    coursedto.instructorid = null;
                }
                return coursedto;
            }
            else
            {
                return null;
            }
        }
        public async Task<List<coursewithstudentscountdto>> getcoursewithstudentscount()
        {
            var courseslist = await _coursedata.getcourseswithstudentscount();
            var coursewithstudentscount = new List<coursewithstudentscountdto>();
            foreach (var course in courseslist)
            {
                coursewithstudentscountdto coursewithstudentscountdto = new coursewithstudentscountdto();
                coursewithstudentscountdto.courseid = course.id;
                coursewithstudentscountdto.coursename = course.name;
                coursewithstudentscountdto.studentscount = course.studentcourses.Count(s => s.student.isactive);

                coursewithstudentscount.Add(coursewithstudentscountdto);
            }
            return coursewithstudentscount;
        }
        public async Task<bool> assigninstructor(int courseid, int instructorid)
        {
            
            bool instructorexist =await _instructordata.exists(instructorid);
            if (!instructorexist)
                throw new Exception("instructor is not found");

            var course=await _coursedata.getcoursebyid(courseid);
            if(course!=null)
            {
                if(course.instructorid==null)
                {
                    return await _coursedata.assigninstructor(courseid, instructorid);

                }
                else
                {
                    throw new Exception("this course have already instructor");
                }
            }
            else
            {
                throw new Exception("course is not found");
            }
        }
        public async Task<bool> updatecourse(int id, newcoursedto newcoursedto)
        {
            bool exist=await _coursedata.exists(id);
            if (!exist)
                throw new Exception("course is not found");

            bool result = false;
            var course = new course
            {
                id = id,
                name = newcoursedto.name,
                hours = newcoursedto.hours,
                maxstudents = newcoursedto.maxstudents,
                instructorid = newcoursedto.instructorid
            };
            result = await _coursedata.updatecourse(course);
            return result;
        }
        public async Task<bool>deletecourse(int id)
        {
            bool exist=await _coursedata.exists(id);
            if (!exist)
                throw new Exception("course is not found");

            return await _coursedata.deletecourse(id);
        }
    }
}
