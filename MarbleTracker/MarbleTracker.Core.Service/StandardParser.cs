using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarbleTracker.Core.Service
{
    public class StandardParser
    {
        private static char[] ENCLOSERS => new char[1] { '"' };
        private static char DELIMITER => ' ';

        public ParseResult<CommandSkeleton> GetCommand(string input)
        {
            var ret = new ParseResult<CommandSkeleton>();

            try
            {
                return new ParseResult<CommandSkeleton>()
                {
                    Value = this.Parse(input),
                    Successful = true
                };
            } catch (Exception e)
            {
                return new ParseResult<CommandSkeleton>()
                {
                    Successful = false,
                    Error = e
                };
            }
        }

        private CommandSkeleton Parse(string input)
        {
            var split = input.Split(DELIMITER);
            var commandName = split[0];
            var recombination = input.Replace(commandName, "").Trim();

            var args = GetArguments(recombination);

            return new CommandSkeleton(commandName, args);
        }

        private IEnumerable<KeyValuePair<string, string>> GetArguments(string input)
        {
            var keyBuilder = new StringBuilder();
            var valueBuilder = new StringBuilder();

            bool keyBuilt = false;

            bool ignoreDelimiters = false;

            for (int i = 0; i < input.Length; i++)
            {
                var currentChar = input[i];

                if (ENCLOSERS.Contains(currentChar))
                {
                    // if we hit an encloser, but we're already ignoring delimiters, then this must be end of our enclosed value.
                    if (ignoreDelimiters)
                    {
                        ignoreDelimiters = false;
                    }
                    else
                    {
                        ignoreDelimiters = true; // otherwise, we must be at the start of our enclosed value.
                    }
                }

                if (!ignoreDelimiters && currentChar == DELIMITER)
                {
                    // We already have a key built, and we've hit the end of our value for said key. Return it.
                    if (keyBuilt)
                    {
                        yield return new KeyValuePair<string, string>(keyBuilder.ToString(), valueBuilder.ToString());

                        keyBuilder.Clear();
                        valueBuilder.Clear();

                        keyBuilt = false;
                        continue; // no need to inclue the delimiter
                    }
                    else // if the key isn't complete, then we've just finished our key. Start building our value.
                    {
                        keyBuilt = true;
                        continue; // no need to include the delimiter
                    }
                }

                if (keyBuilt)
                {
                    valueBuilder.Append(currentChar);
                }
                else
                {
                    keyBuilder.Append(currentChar);
                }

                if (i == input.Length - 1) // we're at the end. Return what we've got
                {
                    yield return new KeyValuePair<string, string>(keyBuilder.ToString(), valueBuilder.ToString());
                }
            }
        }
    }
}
