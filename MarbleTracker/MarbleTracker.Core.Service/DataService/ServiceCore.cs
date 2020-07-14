using MarbleTracker.Core.Data;
using MarbleTracker.Core.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MarbleTracker.Core.Service.DataService
{
    public class ServiceCore
    {
        string Principal;

        CommandLayer CommandService;
        QueryLayer QueryService;

        public ServiceCore(DbContextOptions<MarbleContext> options, string principal)
        {
            this.Principal = principal;

            this.CommandService = new CommandLayer(options, principal);
            this.QueryService = new QueryLayer(options, principal);
        }

        public async Task CreateUser(string username)
        {
            await this.CommandService.CreateUser(username);
        }

        public async Task CreateGroup(string groupName, long userCreatorId)
        {
            await this.CommandService.CreateGroup(groupName, userCreatorId);
        }

        public async Task RemoveGroup(long groupId)
        {
            await this.CommandService.RemoveGroup(groupId);
        }

        public async Task AddUserToGroup(long groupId, long userId)
        {
            await this.CommandService.AddUserToGroup(groupId, userId);
        }

        public async Task RemoveUserFromGroup(long groupId, long userId)
        {
            await this.CommandService.RemoveUserFromGroup(groupId, userId);
        }

        public async Task<User> GetUser(string username)
        {
            return await this.QueryService.GetUser(username);
        }
    }
}
