using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Core.Service
{
    public class CommandResult
    {
        public bool Successful { get; }
        public string Message { get; }

        public CommandResult(bool successful, string message = null)
        {
            this.Successful = successful;
            this.Message = message;
        }
    }
}
