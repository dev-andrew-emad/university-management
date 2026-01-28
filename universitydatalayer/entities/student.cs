using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace universitydatalayer.entities
{
    public class student
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int age { get; set; }
        public DateTime createdat { get; set; }
        public bool isactive {  get; set; }
        public ICollection<studentcourse> studentcourses { get; set; }
    }
}
