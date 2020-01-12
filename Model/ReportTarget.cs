using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoverageReporter.Model
{
    /// <summary>
    /// Report target project informations
    /// </summary>
    public struct ReportTarget
    {
        /// <summary>
        /// project name
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// target project test dll path
        /// </summary>
        public string TargetDll { get; }
        /// <summary>
        /// target project source code root path
        /// </summary>
        public string SourcePath { get; }

        public ReportTarget(string name, string targetDll, string sourcePath)
        {
            this.Name = name;
            this.TargetDll = targetDll;
            this.SourcePath = sourcePath;
        }
    }
}
