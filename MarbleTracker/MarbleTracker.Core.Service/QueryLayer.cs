using MarbleTracker.Core.Data;
using MarbleTracker.Core.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarbleTracker.Core.Service
{
    public class QueryLayer
    {
        DbContextOptions<MarbleContext> Options;
        string Principal;

        public QueryLayer(DbContextOptions<MarbleContext> options, string principal)
        {
            this.Options = options;
            this.Principal = principal;
        }

        public async Task<User> GetUser(string username)
        {
            using (var ctx = new MarbleContext(this.Options))
            {
                return await ctx.Users.FirstOrDefaultAsync(x => x.Username == username);
            }
        }
    }
}
