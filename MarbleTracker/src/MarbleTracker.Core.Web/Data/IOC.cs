using MarbleTracker.Core.Data;
using MarbleTracker.Core.Service.DataService;
using MarbleTracker.Core.Web.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MarbleTracker.Core.Web.Data
{
    public static class IOC
    {
        private static string ConnectionStringKey => ApplicationConfigurationKeys.MarbleDatabaseConnectionString;
        private static string InMemoryDbName => "test";

        public static ServiceCore GetService(IConfiguration configuration, string principal, bool useInMemory = false)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MarbleContext>();
            if (useInMemory)
            {
                optionsBuilder.UseInMemoryDatabase(InMemoryDbName);
            }
            else
            {
                optionsBuilder.UseSqlite(configuration.GetConnectionString(ConnectionStringKey));
            }

            return new ServiceCore(optionsBuilder.Options, principal);
        }

    }
}
