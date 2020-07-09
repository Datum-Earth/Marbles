using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Core.Service.Interfaces
{
    public interface ICommand
    {
        string Name { get; }
        string Description { get; }

        IEnumerable<KeyValuePair<string, string>> Arguments { get; }

        T Execute<T>();
        string GetHelpMessage();
    }
}
