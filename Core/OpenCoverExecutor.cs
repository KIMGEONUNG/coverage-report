using CoverageReporter.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoverageReporter.Core
{
    /// <summary>
    /// OpenCover.exe execution object.
    /// </summary>
    public class OpenCoverExecutor : ExecutorBase
    {
        /// <summary>
        /// options : user,path32,path64
        /// </summary>
        [OptionAttribute]
        public string register = "path32";

        /// <summary>
        /// Test exe file
        /// </summary>
        [OptionAttribute]
        public string target;

        /// <summary>
        /// Test exe file argument string
        /// </summary>
        [OptionAttribute]
        public string targetargs;

        /// <summary>
        /// output file path
        /// </summary>
        [OptionAttribute]
        public string output;
        
        /// <summary>
        /// 
        /// </summary>
        public OpenCoverExecutor(string exePath) : base(exePath)
        {

        }
    }
}
