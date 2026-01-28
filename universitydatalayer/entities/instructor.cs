using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace universitydatalayer.entities
{
    public class instructor
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }

        public ICollection<course> courses { get; set; }
    }
}
