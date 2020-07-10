using MarbleTracker.Core.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Core.Service.Commands
{
    public class GetHistoryCommand : ICommand
    {
        public string Name => "get-history";
        public string Description => "Use to retrieve n last wagers for a given user, in descending order.";
        public bool IsHidden => false;
        public IEnumerable<KeyValuePair<string, string>> Arguments { get; }

        public GetHistoryCommand(IEnumerable<KeyValuePair<string, string>> args)
        {
            this.Arguments = args;
        }

        public string Execute()
        {
            throw new NotImplementedException();
        }

        public string GetHelpMessage()
        {
            return Description + "\r\n" +
                   "Pattern: get-history {username} {take}";
        }
    }
}
