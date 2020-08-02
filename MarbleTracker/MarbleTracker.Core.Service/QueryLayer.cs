using MarbleTracker.Core.Data;
using MarbleTracker.Core.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarbleTracker.Core.Service
{
    public class QueryLayer : IDisposable
    {
        MarbleContext Context;
        string Principal;

        public QueryLayer(DbContextOptions<MarbleContext> options, string principal)
        {
            this.Context = new MarbleContext(options);
            this.Principal = principal;
        }

        public async Task<User> GetUserAsync(string username)
        {
            return await this.Context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<Group> GetGroupAsync(long id)
        {
            return await this.Context.Groups.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<UserGroupRelationship> GetGroupRelationships(long groupId)
        {
            return this.Context.UserGroupRelationships.Where(x => x.GroupId == groupId);
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Context.Dispose();
            }
        }
    }
}
