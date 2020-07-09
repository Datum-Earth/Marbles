using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Core.Service.Interfaces
{
    public interface ICommand
    {
        string Name { get; }
        string Description { get; }
        
        T Execute<T>(string[] args);
        string GetHelpMessage();
    }
}
