using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Core.Service
{
    public class ParseResult<T>
    {
        public T Value { get; set; }
        public bool Successful { get; set; }
        public Exception Error { get; set; }

        public override string ToString()
        {
            var valStr = String.Empty;
            var ex = String.Empty;
            var exStack = String.Empty;

            if (this.Value is object)
            {
                valStr = this.Value.ToString();
            }

            if (this.Error is object)
            {
                ex = this.Error.Message;
                exStack = this.Error.StackTrace;
            }

            var message = $"Value: {valStr}\r\nException Message: {ex}\r\nStack Trace: {exStack}";

            return message;
        }
    }
}
