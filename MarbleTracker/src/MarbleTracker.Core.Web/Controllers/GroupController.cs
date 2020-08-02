using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarbleTracker.Core.Data.Models;
using MarbleTracker.Core.Web.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MarbleTracker.Core.Web.Controllers
{
    [Route("api/group/")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        IConfiguration Configuration;
        string Principal;

        public GroupController(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.Principal = User?.Identity.Name ?? "Anonymous";
        }

        [Route("")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Group>> Get(long id)
        {
            using (var svc = IOC.GetService(this.Configuration, this.Principal))
            {
                return await svc.GetGroup(id);
            }
        }

        [Route("")]
        [HttpPost]
        public async Task Post(string groupName, long userId)
        {
            using (var svc = IOC.GetService(this.Configuration, this.Principal))
            {
                await svc.CreateGroup(groupName, userId);
            }
        }

        [Route("users")]
        [HttpGet]
        public IEnumerable<User> GetUsersForGroup(long groupId)
        {
            using (var svc = IOC.GetService(this.Configuration, this.Principal))
            {
                foreach (var item in svc.GetUsersForGroup(groupId))
                {
                    yield return item;
                }
            }
        }
    }
}
