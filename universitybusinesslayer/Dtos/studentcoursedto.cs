using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace universitybusinesslayer.Dtos
{
    public class studentcoursedto
    {
        public int studentid { get; set; }
        public int courseid { get; set; }
        public DateTime enrolldate { get; set; }
        public int? grade { get; set; }
    }
}
