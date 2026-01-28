using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace universitybusinesslayer.Dtos
{
    public class newstudentdto
    {
        public string name { get; set; }
        public string email { get; set; }
        public int age { get; set; }
        public DateTime createdat { get; set; }
        public bool isactive { get; set; }
    }
}
