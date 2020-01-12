using CoverageReporter.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoverageReporter.Core
{
    public class OpenCoverExecutor : ExecutorBase
    {

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
        
        public OpenCoverExecutor(string exePath) : base(exePath)
        {

        }
    }
}
