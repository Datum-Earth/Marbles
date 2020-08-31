using MarbleTracker.Core.Data;
using MarbleTracker.Core.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarbleTracker.Core.Service.Test
{
    [TestClass]
    public class CoreTests
    {
        private readonly string DbName = "dev";
        private readonly string Principal = "unit_test";
        private readonly DbContextOptions<MarbleContext> Options;

        public CoreTests()
        {
            this.Options = new DbContextOptionsBuilder<MarbleContext>().UseInMemoryDatabase(this.DbName).Options;
        }

        [TestMethod]
        public void Can_Construct_CommandLayer()
        {
            using (var ctx = new CommandLayer(this.Options, this.Principal))
            {
                Assert.IsTrue(ctx is object);
            }
        }

        [TestMethod]
        public void Can_Construct_QueryLayer()
        {
            using (var ctx = new QueryLayer(this.Options, this.Principal))
            {
                Assert.IsTrue(ctx is object);
            }
        }

        [DataTestMethod]
        [DataRow("datum-earth")]
        [DataRow("da611d6d-65d1-421d-a210-462707271f82")]
        [DataRow("")]
        [DataRow("     ")]
        public async Task Can_CreateAndRetrieve_User(string username)
        {
            using (var ctx = new MarbleContext(this.Options))
            using (var cmd = new CommandLayer(ctx, this.Principal))
            using (var query = new QueryLayer(ctx, this.Principal))
            {
                await cmd.CreateUser(username);

                var user = await query.GetUserAsync(username);

                Assert.IsTrue(user is object);
                Assert.IsTrue(user.Username == username);
                Assert.IsTrue(user.Principal == this.Principal);
                Assert.IsTrue(user.MarbleAmount == 0m);
            }
        }

        [DataTestMethod]
        [DataRow("dunk boyz")]
        public async Task Can_CreateAndRetrieve_Group(string groupName)
        {
            using (var ctx = new MarbleContext(this.Options))
            using (var cmd = new CommandLayer(ctx, this.Principal))
            using (var query = new QueryLayer(ctx, this.Principal))
            {
                var username = "datum-earth";
                await cmd.CreateUser(username);

                var user = await query.GetUserAsync(username);

                await cmd.CreateGroup(groupName, user.Id);
                var groupId = ctx.Groups.FirstOrDefault().Id;

                var group = await query.GetGroupAsync(groupId);

                Assert.IsTrue(group is object);
                Assert.IsTrue(group.Name == groupName);
            }
        }

        [TestMethod]
        public async Task Can_CreateAndDelete_Group()
        {
            using (var ctx = new MarbleContext(this.Options))
            using (var cmd = new CommandLayer(ctx, this.Principal))
            using (var query = new QueryLayer(ctx, this.Principal))
            {
                var username = "datum-earth";
                await cmd.CreateUser(username);

                var user = await query.GetUserAsync(username);

                await cmd.CreateGroup("test", user.Id);
                var groupId = ctx.Groups.FirstOrDefault().Id;

                await cmd.RemoveGroup(groupId);

                var actual = await query.GetGroupAsync(groupId);
                Group expected = null;

                Assert.AreEqual(actual, expected);
            }
        }
    }
}
