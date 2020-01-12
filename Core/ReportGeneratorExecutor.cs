using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoverageReporter.Core
{
    public class ReportGeneratorExecutor : ExecutorBase
    {
        [OptionAttribute]
        public string reports;
        [OptionAttribute]
        public string targetdir;
        [OptionAttribute]
        public string sourcedir;
        
        public ReportGeneratorExecutor(string exePath) : base(exePath)
        {
        }
    }
}
