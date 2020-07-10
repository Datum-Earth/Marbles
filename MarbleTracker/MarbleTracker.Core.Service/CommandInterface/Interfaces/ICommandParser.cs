using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Core.Service.Interfaces
{
    public interface ICommandParser<T>
    {
        ParseResult<T> GetCommand(string input);
    }
}
