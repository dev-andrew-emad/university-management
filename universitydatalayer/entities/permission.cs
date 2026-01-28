using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace universitydatalayer.entities
{
    public class permission
    {
        public int id {  get; set; }
        public string name { get; set; }
        public ICollection<userpermission> userpermissions { get; set; }

    }
}
