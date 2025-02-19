using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace depotakipuyg
{
    internal class database
    {
        public static string GetConnectionString
        {
            get { return "Server=DESKTOP-LTS3TRS; Initial Catalog = muhasebe; Integrated Security = True;"; }
        }
    }
}
