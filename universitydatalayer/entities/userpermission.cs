using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace universitydatalayer.entities
{
    public class userpermission
    {
        public int userid {  get; set; }
        public user user { get; set; }
        public int permissionid {  get; set; }
        public permission permission { get; set; }
    }
}
