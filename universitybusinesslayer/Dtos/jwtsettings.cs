using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace universitybusinesslayer.Dtos
{
    public class jwtsettings
    {
        public string key {  get; set; }
        public string issuer {  get; set; }
        public string audience {  get; set; }
        public double durationinhours {  get; set; }
    }
}
