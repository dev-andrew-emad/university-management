using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace universitybusinesslayer.Dtos
{
    public class newcoursedto
    {
        public string name { get; set; }
        public int hours { get; set; }
        public int maxstudents { get; set; }

        public int? instructorid { get; set; }
    }
}
