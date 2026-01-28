using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace universitybusinesslayer.Dtos
{
    public class pagedresultdto<T>
    {
        public int pagenumber { get; set; }
        public int pagesize { get; set; }
        public int totalcount { get; set; }
        public int totalpages { get; set; }
        public List<T> data { get; set; }
    }
}
