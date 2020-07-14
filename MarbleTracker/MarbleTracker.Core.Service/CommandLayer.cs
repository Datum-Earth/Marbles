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
    public class CommandLayer
    {
        DbContextOptions<MarbleContext> Options;
        string Principal;

        public CommandLayer(DbContextOptions<MarbleContext> options, string principal)
        {
            this.Options = options;
            this.Principal = principal;
        }

        public async Task CreateUser(string username)
        {
            using (var ctx = new MarbleContext(this.Options))
            {
                if (await ctx.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Username == username) == null)
                {
                    ctx.Users.Add(new User()
                    {
                        Username = username,
                        DateCreated = DateTimeOffset.UtcNow,
                        MarbleAmount = 0,
                        Principal = this.Principal
                    });

                    await ctx.SaveChangesAsync();
                }
            }
        }

        public async Task CreateGroup(string groupName, long userCreatorId)
        {
            using (var ctx = new MarbleContext(this.Options))
            {
                var userCreator = await ctx.Users.FindAsync(userCreatorId);
                var newGroup = new Group()
                {
                    Name = groupName,
                    DateCreated = DateTimeOffset.UtcNow,
                    Principal = this.Principal
                };

                newGroup.Relationships.Add(new UserGroupRelationship() 
                { 
                    User = userCreator, 
                    Group = newGroup,
                    DateCreated = DateTimeOffset.UtcNow,
                    Principal = this.Principal
                });

                ctx.Groups.Add(newGroup);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task RemoveGroup(long groupId)
        {
            using (var ctx = new MarbleContext(this.Options))
            {
                var group = await ctx.Groups.FindAsync(groupId);

                ctx.Groups.Remove(group);
            }
        }

        public async Task AddUserToGroup(long groupId, long userId)
        {
            using (var ctx = new MarbleContext(this.Options))
            {
                if (await ctx.UserGroupRelationships.FirstOrDefaultAsync(x => x.GroupId == groupId && x.UserId == userId) == null)
                {
                    await ctx.UserGroupRelationships.AddAsync(new UserGroupRelationship()
                    {
                        UserId = userId,
                        GroupId = groupId,
                        DateCreated = DateTimeOffset.UtcNow,
                        Principal = this.Principal
                    });

                    await ctx.SaveChangesAsync();
                }
            }
        }

        public async Task RemoveUserFromGroup(long groupId, long userId)
        {
            using (var ctx = new MarbleContext(this.Options))
            {
                var targetRelationship = await ctx.UserGroupRelationships.Where(x => x.GroupId == groupId && x.UserId == userId).FirstOrDefaultAsync();

                if (targetRelationship is object)
                {
                    ctx.UserGroupRelationships.Remove(targetRelationship);
                    await ctx.SaveChangesAsync();
                }
            }
        }
    }
}
