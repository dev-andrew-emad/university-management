using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace universitydatalayer.entities
{
    public class course
    {
        public int id { get; set; }
        public string name { get; set; }
        public int hours { get; set; }
        public int maxstudents { get; set; }
        public ICollection<studentcourse> studentcourses { get; set; }

        public int? instructorid { get; set; }
        public instructor? instructor { get; set; }
    }
}
