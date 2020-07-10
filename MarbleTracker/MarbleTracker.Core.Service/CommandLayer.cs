using MarbleTracker.Core.Data;
using MarbleTracker.Core.Data.Models;
using MarbleTracker.Core.Service.ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<CommandResult> CreateGroup(string groupName, long userCreatorId)
        {
            using (var ctx = new MarbleContext(this.Options))
            {
                if (await ctx.Groups.AsNoTracking().SingleOrDefaultAsync(x => x.Name == groupName) == null)
                {
                    var userCreator = await ctx.Users.FindAsync(userCreatorId);
                    if (userCreator is object)
                    {
                        var newGroup = new Group()
                        {
                            Name = groupName,
                            DateCreated = DateTimeOffset.UtcNow,
                            Principal = this.Principal
                        };

                        newGroup.Users.Add(userCreator);

                        ctx.Groups.Add(newGroup);
                        await ctx.SaveChangesAsync();

                        return new CommandResult(true);
                    }
                    else
                    {
                        return new CommandResult(false, ErrorMessages.USER_INVALID);
                    }
                }
                else
                {
                    return new CommandResult(false, ErrorMessages.GROUP_ALREADY_EXISTS);
                }
            }
        }
    }
}
