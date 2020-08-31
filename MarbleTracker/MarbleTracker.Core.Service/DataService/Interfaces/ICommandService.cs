using MarbleTracker.Core.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarbleTracker.Core.Service
{
    public interface ICommandService
    {
        Task CreateUserAsync(string userName);
        Task CreateGroupAsync(string groupName, long userCreatorId);
        Task RemoveGroupAsync(long groupId);
        Task AddUserToGroupAsync(long groupId, long userId);
        Task RemoveUserFromGroupAsync(long groupId, long userId);
        Task CreateChallengeAsync(long initiatingGroupId, long targetGroupId, long witnessId, ChallengeType type);
        Task AddWagerAsync(long challengeId, long userId, decimal amount);
        Task InitiateChallengeAsync(long challengeId);
        Task ConcludeChallengeAsync(long challengeId, ChallengeResult result);
        Task SaveChangesAsync();
    }
}
