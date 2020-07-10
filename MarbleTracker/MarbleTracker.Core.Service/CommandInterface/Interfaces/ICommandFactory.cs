using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Core.Service.Interfaces
{
    public interface ICommandFactory
    {
        ICommand GetCommand(string input);
    }
}
