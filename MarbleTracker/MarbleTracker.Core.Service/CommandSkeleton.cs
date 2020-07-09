using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MarbleTracker.Core.Service
{
    public class CommandSkeleton
    {
        public string CommandName { get; }
        public IEnumerable<KeyValuePair<string, string>> Arguments { get; }

        public CommandSkeleton(string commandName, IEnumerable<KeyValuePair<string, string>> args)
        {
            this.CommandName = commandName;
            this.Arguments = args;
        }

        public override string ToString()
        {
            List<string> combinedArguments = new List<string>();

            foreach (var kvp in this.Arguments)
            {
                combinedArguments.Add(kvp.Key + ' ' + kvp.Value);
            }

            var args = String.Join(' ', combinedArguments);

            return this.CommandName + ' ' + args;
        }
    }
}
