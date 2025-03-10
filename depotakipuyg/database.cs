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
            get { return "Server=DESKTOP-LTS3TRS; Initial Catalog = muhasebe2; Integrated Security = True;"; }
            //get { return "Server=DESKTOP-RAPE8QS\\MSSQLSERVER01; Initial Catalog = muhasebe; Integrated Security = True;"; }
        }
    }
}
