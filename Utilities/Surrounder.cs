using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoverageReporter.Utilities
{
    /// <summary>
    /// Manipulate string with custom intention
    /// </summary>
    public class StringModifier
    {
        /// <summary>
        /// Surround for target string with specific string 
        /// </summary>
        /// <param name="surround">surrounder</param>
        /// <param name="contents">surrounded contents</param>
        /// <returns></returns>
        public static string SurroundWith(string surround, string contents)
        {
            return surround + contents + surround;
        }
    }
}
