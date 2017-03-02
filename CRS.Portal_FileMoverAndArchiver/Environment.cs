using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRS.Portal_FileMoverAndArchiver
{
    /// <summary>
    /// Contains properties associated with the CRS Portal File Mover environment
    /// </summary>
    public class Environment
    {
        public static string Name = ConfigurationManager.AppSettings["Environment"].ToString(CultureInfo.InvariantCulture); 
    }
}