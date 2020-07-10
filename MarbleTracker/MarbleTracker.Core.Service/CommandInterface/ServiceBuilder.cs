using MarbleTracker.Core.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Core.Service
{
    public class ServiceBuilder
    {
        public ServiceStrategy Strategy { get; }

        public ServiceBuilder(ServiceStrategy strategy)
        {
            this.Strategy = strategy;
        }

        public ICommandExecutor GetExecutor()
        {
            switch (this.Strategy)
            {
                case ServiceStrategy.Standard:
                    return new CommandExecutor(new CommandFactory(new StandardParser()));
                default:
                    throw new NotSupportedException();
            }
        }

        public ICommandFactory GetFactory()
        {
            switch (this.Strategy)
            {
                case ServiceStrategy.Standard:
                    return new CommandFactory(new StandardParser());
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
