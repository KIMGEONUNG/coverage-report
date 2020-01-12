using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoverageReporter.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public class Surrounder
    {
        public static string SurroundWith(string surround, string contents)
        {
            return surround + contents + surround;
        }
    }
}
