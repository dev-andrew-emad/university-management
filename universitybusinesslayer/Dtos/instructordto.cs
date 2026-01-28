using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using universitydatalayer.entities;

namespace universitybusinesslayer.Dtos
{
    public class instructordto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public List<string> courses { get; set; }
    }
}
