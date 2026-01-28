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
    public class studentcoursebusiness
    {
        private readonly studentcoursesdata _studentcoursedata;
        private readonly studentdata _studentdata;
        public studentcoursebusiness(studentcoursesdata studentcoursedata, studentdata studentdata)
        {
            _studentcoursedata = studentcoursedata;
            _studentdata = studentdata;
        }
        public async Task enrollstudent(studentcoursedto studentcoursedto)
        {
            bool exist = await _studentcoursedata.exists(studentcoursedto.studentid, studentcoursedto.courseid);
            if (exist)
                throw new Exception("this student is already enrolled to this course");

            bool studentexist = await _studentdata.exists(studentcoursedto.studentid);
            if (!studentexist)
                throw new Exception("this student is not found");

            var course = await _studentcoursedata.getcoursewithstudents(studentcoursedto.courseid);
            if (course == null)
                throw new Exception("this course is not found");

            int activestudents = course.studentcourses.Count(s => s.student.isactive == true);

            if (activestudents >= course.maxstudents)
                throw new Exception("this course have max students");
            var studentcourse = new studentcourse
            {
                studentid = studentcoursedto.studentid,
                courseid = studentcoursedto.courseid,
                enrolldate = studentcoursedto.enrolldate,
                grade = studentcoursedto.grade
            };
            await _studentcoursedata.enrollstudent(studentcourse);

        }
    }
}
