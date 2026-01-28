using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace universitydatalayer.entities
{
    public class user
    {
       public int id {  get; set; }
        public string email {  get; set; }
        public string password { get; set; }
        public string role {  get; set; }

        public ICollection<userpermission>userpermissions { get; set; }
    }
}
