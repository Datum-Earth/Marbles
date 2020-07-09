using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MarbleTracker.Core.Service.Test
{
    [TestClass]
    public class Parsing
    {
        private List<string> Cases => new List<string>()
        {
            
        };

        [TestMethod]
        public void SingleArgumentLongForm()
        {
            var input = "get-history --username @datum-earth";

            var expected = new ParseResult<CommandSkeleton>()
            {
                Value = new CommandSkeleton("get-history", new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("--username", "@datum-earth") }),
                Successful = true,
                Error = null
            };

            var parser = new StandardParser();

            var actual = parser.GetCommand(input);

            Assert.IsTrue(ResultsAreEqual(expected, actual));
        }

        [TestMethod]
        public void SingleArgumentShortForm()
        {
            var input = "get-history -u @datum-earth";

            var expected = new ParseResult<CommandSkeleton>()
            {
                Value = new CommandSkeleton("get-history", new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("-u", "@datum-earth") }),
                Successful = true,
                Error = null
            };

            var parser = new StandardParser();

            var actual = parser.GetCommand(input);

            Assert.IsTrue(ResultsAreEqual(expected, actual));
        }

        [TestMethod]
        public void TwoArgumentsLongForm()
        {
            var input = "get-history --username @datum-earth --take 20";

            var expected = new ParseResult<CommandSkeleton>()
            {
                Value = new CommandSkeleton("get-history", new List<KeyValuePair<string, string>>() 
                { 
                    new KeyValuePair<string, string>("--username", "@datum-earth"),
                    new KeyValuePair<string, string>("--take", "20")
                }),
                Successful = true,
                Error = null
            };

            var parser = new StandardParser();

            var actual = parser.GetCommand(input);

            Assert.IsTrue(ResultsAreEqual(expected, actual));
        }

        private bool ResultsAreEqual(ParseResult<CommandSkeleton> L, ParseResult<CommandSkeleton> R)
        {
            if (!L.Successful.Equals(R.Successful))
                return false;
            
            if (L.Error is object && R.Error is object)
            {
                if (!L.Error.Equals(R.Error))
                    return false;
            }
            
            if (!CommandsAreEqual(L.Value, R.Value))
                return false;

            return true;
        }

        private bool CommandsAreEqual(CommandSkeleton L, CommandSkeleton R)
        {
            if (L is object && R is object)
            {
                if (!L.CommandName.Equals(R.CommandName))
                    return false;

                if (L.Arguments.Count() != R.Arguments.Count())
                    return false;

                for (int i = 0; i < L.Arguments.Count(); i++)
                {
                    var li = L.Arguments.ElementAt(i);
                    var ri = R.Arguments.ElementAt(i);

                    if (!li.Key.Equals(ri.Key))
                        return false;

                    if (!li.Value.Equals(ri.Value))
                        return false;
                }
            }
            else if (L is object && R == null || L == null && R is object)
            {
                return false;
            }

            return true;
        }
    }
}
