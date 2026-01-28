using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace universitydatalayer.entities
{
    public class studentcourse
    {
        public int studentid { get; set; }
        public student student { get; set; }
        public int courseid { get; set; }
        public course course { get; set; }
        public DateTime enrolldate { get; set; }
        public int? grade { get; set; }
    }
}
