using MarbleTracker.Core.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Core.Service.Commands
{
    public class GetHistory : ICommand
    {
        public string Name => "get-history";
        public string Description => "Use to retrieve n last wagers for a given user, in descending order.";

        public T Execute<T>(string[] args)
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
