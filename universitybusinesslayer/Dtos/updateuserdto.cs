using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace universitybusinesslayer.Dtos
{
    public class updateuserdto
    {
        public string email {  get; set; }
        public string role {  get; set; }
        public List<int>? permissionids { get; set; }
    }
}
