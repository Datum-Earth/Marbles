using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Core.Service.Interfaces
{
    public interface ICommand
    {
        string Name { get; }
        string Description { get; }
        bool IsHidden { get; }

        IEnumerable<KeyValuePair<string, string>> Arguments { get; }

        string Execute();
        string GetHelpMessage();
    }
}
