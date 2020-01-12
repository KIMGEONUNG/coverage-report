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
    public abstract class ExecutorBase
    {
        protected string _exePath;

        public ExecutorBase(string exePath)
        {
            _exePath = exePath;
        }

        public string GetExePath()
        {
            return Surrounder.SurroundWith("\"", _exePath);
        }

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
                    string surroundedValue = Surrounder.SurroundWith("\"", value);
                    string op = $"-{optionName}:{surroundedValue}";

                    ops.Add(op);
                }
            }

            string cmd = string.Join(" ", ops);

            return cmd;

        }

        public void Execute()
        {
            string exePath = GetExePath();
            string args = GetArgsString();

            ProcessStartInfo start = new ProcessStartInfo(exePath, args);

            Process process = Process.Start(start);
            process.WaitForExit();
        }

        [AttributeUsage(AttributeTargets.Field)]
        protected class OptionAttribute : Attribute
        {
            public OptionAttribute()
            {

            }
        }
    }
}
