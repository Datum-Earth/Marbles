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
    public class CommandLayer : ICommandService, IDisposable
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

        public async Task CreateUserAsync(string username)
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
            }
        }

        public async Task CreateGroupAsync(string groupName, long userCreatorId)
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
        }

        public async Task RemoveGroupAsync(long groupId)
        {
            var group = await this.Context.Groups.FindAsync(groupId);

            this.Context.Groups.Remove(group);
        }

        public async Task AddUserToGroupAsync(long groupId, long userId)
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
            }
        }

        public async Task RemoveUserFromGroupAsync(long groupId, long userId)
        {
            var targetRelationship = await this.Context.UserGroupRelationships.Where(x => x.GroupId == groupId && x.UserId == userId).FirstOrDefaultAsync();

            if (targetRelationship is object)
            {
                this.Context.UserGroupRelationships.Remove(targetRelationship);
            }
        }

        public async Task CreateChallengeAsync(long initiatingGroupId, long targetGroupId, long witnessId, ChallengeType type)
        {
            var sourceGroup = await this.Context.Groups.FirstOrDefaultAsync(x => x.Id == initiatingGroupId);
            var targetGroup = await this.Context.Groups.FirstOrDefaultAsync(x => x.Id == targetGroupId);
            var witness = await this.Context.Users.FirstOrDefaultAsync(x => x.Id == witnessId);

            var newChallenge = new Challenge()
            {
                DateCreated = DateTime.UtcNow,
                Principal = this.Principal,
                SourceGroup = sourceGroup,
                TargetGroup = targetGroup,
                Status = ChallengeStatus.Lobbying,
                Type = type,
                Witness = witness
            };

            this.Context.Add(newChallenge);
        }

        public async Task AddWagerAsync(long challengeId, long userId, decimal amount)
        {
            var targetChallenge = await this.Context.Challenges.FirstOrDefaultAsync(x => x.Id == challengeId);
            var targetUser = await this.Context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            var newWager = new Wager()
            {
                DateCreated = DateTime.UtcNow,
                Challenge = targetChallenge,
                Amount = amount,
                Principal = this.Principal,
                User = targetUser
            };

            await this.Context.Wagers.AddAsync(newWager);
        }

        public async Task InitiateChallengeAsync(long challengeId)
        {
            var targetChallenge = await this.Context.Challenges.FirstOrDefaultAsync(x => x.Id == challengeId);

            targetChallenge.Status = ChallengeStatus.InProgress;
        }

        public async Task ConcludeChallengeAsync(long challengeId, ChallengeResult result)
        {
            var targetChallenge = await this.Context.Challenges.FirstOrDefaultAsync(x => x.Id == challengeId);
            targetChallenge.Status = ChallengeStatus.Concluded;
            targetChallenge.Result = result;

            // time to handle resulting transactions
            var wagers = this.Context.Wagers.Where(x => x.Challenge == targetChallenge);

            var sourceUsers = targetChallenge.SourceGroup.Relationships.Select(x => x.User);
            var targetUsers = targetChallenge.TargetGroup.Relationships.Select(x => x.User);
            var sourceWagers = wagers.Where(x => sourceUsers.Contains(x.User));
            var targetWagers = wagers.Where(x => targetUsers.Contains(x.User));

            if (result == ChallengeResult.Tie || result == ChallengeResult.Cancelled)
            {
                this.Context.Wagers.RemoveRange(wagers);
            }
            else if (result == ChallengeResult.SourceWon)
            {
                var totalWagerAmount = sourceWagers.Sum(x => x.Amount);
                var winningsAmount = totalWagerAmount / targetUsers.Count(); // split evenly for all winners

                foreach (var user in targetUsers)
                {
                    user.MarbleAmount += winningsAmount;
                }

                foreach (var wager in sourceWagers)
                {
                    var user = wager.User;
                    user.MarbleAmount -= wager.Amount; // remove what the user wagered
                }
            }
            else if (result == ChallengeResult.TargetWon)
            {
                var totalWagerAmount = targetWagers.Sum(x => x.Amount);
                var winningsAmount = totalWagerAmount / sourceUsers.Count(); // split evenly for all winners

                foreach (var user in sourceUsers)
                {
                    user.MarbleAmount += winningsAmount;
                }

                foreach (var wager in targetWagers)
                {
                    var user = wager.User;
                    user.MarbleAmount -= wager.Amount; // remove what the user wagered
                }
            }

            // now clean up all wagers since they are concluded
            this.Context.Wagers.RemoveRange(wagers);
        }

        public async Task SaveChangesAsync()
        {
            await this.Context.SaveChangesAsync();
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
