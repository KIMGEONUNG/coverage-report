using CoverageReporter.Core;
using CoverageReporter.Model;
using CoverageReporter.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoverageReporter
{
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string reportGeneratorExePath = Config.report_generator_path;
            string openCoverExePath = Config.open_cover_path;
            string vsTestExePath = Config.vs_test_path;

            foreach (string path in new string[] { reportGeneratorExePath, openCoverExePath , vsTestExePath })
            {
                if (!File.Exists(path))
                {
                    throw new Exception($"{path} does not exist.");
                }
            }

            string coverageResultPath = Config.coverage_path;
            string reportResultPath = Config.report_path;
            foreach (string path in new string[] { coverageResultPath, reportResultPath })
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }

            ReportTarget[] targets = Config.ReportTargets;
            foreach (ReportTarget target in targets)
            {
                #region Init

                string name = target.Name;
                string source = target.SourcePath;
                string dllPath = target.TargetDll;

                string targetProjectReportResultPath = Path.Combine(reportResultPath, name);
                string targetProjectCoverageResultPath = Path.Combine(coverageResultPath, name);
                if (!Directory.Exists(targetProjectReportResultPath))
                {
                    Directory.CreateDirectory(targetProjectReportResultPath);
                }

                if (!Directory.Exists(targetProjectCoverageResultPath))
                {
                    Directory.CreateDirectory(targetProjectCoverageResultPath);
                }


                string xmlOutput = Path.Combine(targetProjectCoverageResultPath, Path.ChangeExtension(name, "xml"));
                var openCover = new OpenCoverExecutor(openCoverExePath)
                {
                    target = vsTestExePath,
                    targetargs = dllPath,
                    output = xmlOutput,
                };
                var reportGenerator = new ReportGeneratorExecutor(reportGeneratorExePath)
                {
                    reports = xmlOutput,
                    sourcedir = source,
                    targetdir = targetProjectReportResultPath,
                };
                #endregion

                Console.WriteLine($"Start project : {name}");
                try
                {
                    Console.WriteLine("Coverage inspection start");
                    openCover.Execute();
                    Console.WriteLine("Coverage inspection end");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine($"Failed");
                    continue;
                }
                try
                {
                    Console.WriteLine("Report Gen start");
                    reportGenerator.Execute();
                    Console.WriteLine("Report Gen end");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine($"Failed");
                    continue;
                }
                Console.WriteLine($"Successfully finished");
            }
        }
    }
}
