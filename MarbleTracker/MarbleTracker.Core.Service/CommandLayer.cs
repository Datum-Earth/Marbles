using MarbleTracker.Core.Data;
using MarbleTracker.Core.Data.Models;
using MarbleTracker.Core.Service.ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MarbleTracker.Core.Service
{
    public class CommandLayer : IDisposable
    {
        MarbleContext Context;
        string Principal;

        public CommandLayer(DbContextOptions<MarbleContext> options, string principal)
        {
            this.Context = new MarbleContext(options);
            this.Principal = principal;
        }

        public CommandLayer(MarbleContext context, string principal)
        {
            this.Context = context;
            this.Principal = principal;
        }

        public async Task CreateUser(string username)
        {
            if (await this.Context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Username == username) == null)
            {
                this.Context.Users.Add(new User()
                {
                    Username = username,
                    DateCreated = DateTimeOffset.UtcNow,
                    MarbleAmount = 0,
                    Principal = this.Principal
                });

                await this.Context.SaveChangesAsync();
            }
        }

        public async Task CreateGroup(string groupName, long userCreatorId)
        {
            var userCreator = await this.Context.Users.FindAsync(userCreatorId);
            var newGroup = new Group()
            {
                Name = groupName,
                DateCreated = DateTimeOffset.UtcNow,
                Relationships = new List<UserGroupRelationship>(),
                Principal = this.Principal
            };

            newGroup.Relationships.Add(new UserGroupRelationship() 
            { 
                User = userCreator, 
                Group = newGroup,
                DateCreated = DateTimeOffset.UtcNow,
                Principal = this.Principal
            });

            this.Context.Groups.Add(newGroup);
            await this.Context.SaveChangesAsync();
        }

        public async Task RemoveGroup(long groupId)
        {
            var group = await this.Context.Groups.FindAsync(groupId);

            this.Context.Groups.Remove(group);
        }

        public async Task AddUserToGroup(long groupId, long userId)
        {
            if (await this.Context.UserGroupRelationships.FirstOrDefaultAsync(x => x.GroupId == groupId && x.UserId == userId) == null)
            {
                await this.Context.UserGroupRelationships.AddAsync(new UserGroupRelationship()
                {
                    UserId = userId,
                    GroupId = groupId,
                    DateCreated = DateTimeOffset.UtcNow,
                    Principal = this.Principal
                });

                await this.Context.SaveChangesAsync();
            }
        }

        public async Task RemoveUserFromGroup(long groupId, long userId)
        {
            var targetRelationship = await this.Context.UserGroupRelationships.Where(x => x.GroupId == groupId && x.UserId == userId).FirstOrDefaultAsync();

            if (targetRelationship is object)
            {
                this.Context.UserGroupRelationships.Remove(targetRelationship);
                await this.Context.SaveChangesAsync();
            }
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
