using MarbleTracker.Core.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MarbleTracker.Core.Service.Commands
{
    public class TestCommand : ICommand
    {
        public string Name => "do-test";

        public string Description => "Returns the input of whatever was passed in.";

        public bool IsHidden => true;

        public IEnumerable<KeyValuePair<string, string>> Arguments { get; }

        public TestCommand(IEnumerable<KeyValuePair<string, string>> args)
        {
            this.Arguments = args;
        }

        public string Execute()
        {
            var input = this.Arguments.FirstOrDefault(x => x.Key == "--input" || x.Key == "-i");

            if (String.IsNullOrWhiteSpace(input.Value))
            {
                return "You didn't provide any input, dummy!";
            }
            else
            {
                return input.Value;
            }
        }

        public string GetHelpMessage()
        {
            return "Arguments: --input / -i (\"*\") ";
        }
    }
}
