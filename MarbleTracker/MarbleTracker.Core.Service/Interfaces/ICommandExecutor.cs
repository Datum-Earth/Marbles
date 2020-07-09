using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Core.Service.Interfaces
{
    public interface ICommandExecutor
    {
        string Execute(string input);
    }
}
