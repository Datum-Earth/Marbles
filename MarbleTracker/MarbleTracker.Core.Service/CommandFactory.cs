using MarbleTracker.Core.Service.Commands;
using MarbleTracker.Core.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Core.Service
{
    public class CommandFactory
    {
        ICommandParser<CommandSkeleton> Parser;

        public CommandFactory(ICommandParser<CommandSkeleton> parser)
        {
            this.Parser = parser;
        }

        public T Execute<T>(string input)
        {
            var command = this.GetCommand(input);
            return command.Execute<T>();
        }

        public ICommand GetCommand(string input)
        {
            var parsed = this.Parser.GetCommand(input);

            if (!parsed.Successful)
            {
                throw new ArgumentOutOfRangeException();
            }

            switch (parsed.Value.CommandName)
            {
                case "get-history":
                    return new GetHistoryCommand(parsed.Value.Arguments);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
