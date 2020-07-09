using MarbleTracker.Core.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Core.Service
{
    public class CommandExecutor : ICommandExecutor
    {
        ICommandFactory CommandFactory { get; }

        public CommandExecutor(ICommandFactory factory)
        {
            this.CommandFactory = factory;
        }

        public string Execute(string input)
        {
            return this.CommandFactory.GetCommand(input).Execute();
        }
    }
}
