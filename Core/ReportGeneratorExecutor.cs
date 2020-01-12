using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoverageReporter.Core
{
    /// <summary>
    /// ReportGenerator..exe execution object
    /// </summary>
    public class ReportGeneratorExecutor : ExecutorBase
    {
        /// <summary>
        /// coverage xml path
        /// </summary>
        [OptionAttribute]
        public string reports;

        /// <summary>
        /// Path where results will be stored
        /// </summary>
        [OptionAttribute]
        public string targetdir;

        /// <summary>
        /// source code path
        /// </summary>
        [OptionAttribute]
        public string sourcedir;

        /// <summary>
        /// 
        /// </summary>
        public ReportGeneratorExecutor(string exePath) : base(exePath)
        {
        }
    }
}
