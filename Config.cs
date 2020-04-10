using CoverageReporter.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoverageReporter
{
    /// <summary>
    /// All about program configuration
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Configuration json schema string
        /// </summary>
        public static readonly string ConfigJsonSchema;

        /// <summary>
        /// Configuration json string
        /// </summary>
        public static readonly string ConfigJson;

        /// <summary>
        /// Coverage target json schema string
        /// </summary>
        public static readonly string TargetJsonSchema;

        /// <summary>
        /// Coverage target json string
        /// </summary>
        public static readonly string TargetJson;

        /// <summary>
        /// ReportGenerator execution path
        /// </summary>
        public static readonly string report_generator_path;

        /// <summary>
        /// OpenCover execution program path
        /// </summary>
        public static readonly string open_cover_path;

        /// <summary>
        /// Test execution program path
        /// </summary>
        public static readonly string vs_test_path;

        /// <summary>
        /// Coverage xml file root directory path
        /// </summary>
        public static readonly string coverage_path;
        /// <summary>
        /// Report result root directory path
        /// </summary>
        public static readonly string report_path;

        /// <summary>
        /// target projects where the auto-generated report expected
        /// </summary>
        public static readonly ReportTarget[] ReportTargets;

        static Config()
        {
            string currentDllPath = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine($"App root : {currentDllPath}");
            string jsonConfigPath =Path.Combine(currentDllPath, @"config.json");
            string jsonConfigSchemaPath = Path.Combine(currentDllPath, @"config_schema.json");
            string jsonTargetPath = Path.Combine(currentDllPath, @"targets.json");
            string jsonTargetSchemaPath = Path.Combine(currentDllPath, @"targets_schema.json");

            using (var sr = new StreamReader(jsonConfigPath))
            {
                string str = sr.ReadToEnd();
                ConfigJson = str;
            }

            using (var sr = new StreamReader(jsonConfigSchemaPath))
            {
                string str = sr.ReadToEnd();
                ConfigJsonSchema = str;
            }

            using (var sr = new StreamReader(jsonTargetPath))
            {
                string str = sr.ReadToEnd();
                TargetJson = str;
            }

            using (var sr = new StreamReader(jsonTargetSchemaPath))
            {
                string str = sr.ReadToEnd();
                TargetJsonSchema = str;
            }

            JObject configJo = JObject.Parse(ConfigJson);
            foreach (KeyValuePair<string, JToken> item in configJo)
            {
                string name = item.Key;
                string val = item.Value.Value<string>();

                FieldInfo field = typeof(Config).GetField(name, BindingFlags.Public |BindingFlags.Static);
                field.SetValue(null, val);
            }

            JObject targetsJo = JObject.Parse(TargetJson);
            JArray targets = (JArray)(targetsJo["projects"]);
            var targetsList = new List<ReportTarget>();
            foreach (JObject target in targets)
            {
                string name = target["name"].Value<string>();
                string dll = target["dll"].Value<string>();
                string source = target["sourcedir"].Value<string>();
                var reportTarget = new ReportTarget(name, dll, source);
                targetsList.Add(reportTarget);
            }
            ReportTargets = targetsList.ToArray();

        }
    }
}

