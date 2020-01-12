using CoverageReporter.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoverageReporter.Core
{
    /// <summary>
    /// Abstract class for all executors
    /// </summary>
    public abstract class ExecutorBase
    {
        /// <summary>
        /// Target exe file path
        /// </summary>
        protected string _exePath;

        /// <summary>
        /// 
        /// </summary>
        public ExecutorBase(string exePath)
        {
            _exePath = exePath;
        }

        /// <summary>
        /// Exe file path surrounded by " charater
        /// </summary>
        public string GetExePath()
        {
            return StringModifier.SurroundWith("\"", _exePath);
        }

        /// <summary>
        /// Auto-generated argument  string
        /// </summary>
        public string GetArgsString()
        {
            var ops = new List<string>();
            foreach (FieldInfo item in this.GetType().GetFields())
            {
                var atts = item.CustomAttributes;
                bool hasIt = atts.ToList().Exists(n => n.AttributeType ==  typeof(OptionAttribute));

                if (hasIt)
                {
                    string optionName = item.Name;
                    string value = (string)item.GetValue(this);
                    string surroundedValue = StringModifier.SurroundWith("\"", value);
                    string op = $"-{optionName}:{surroundedValue}";

                    ops.Add(op);
                }
            }

            string cmd = string.Join(" ", ops);

            return cmd;

        }

        /// <summary>
        /// Execute the target exe program
        /// </summary>
        public void Execute()
        {
            string exePath = GetExePath();
            string args = GetArgsString();

            ProcessStartInfo start = new ProcessStartInfo(exePath, args);

            Process process = Process.Start(start);
            process.WaitForExit();
        }

        /// <summary>
        /// 
        /// </summary>
        [AttributeUsage(AttributeTargets.Field)]
        protected class OptionAttribute : Attribute
        {
            /// <summary>
            /// Default constructor
            /// </summary>
            public OptionAttribute()
            {

            }
        }
    }
}
