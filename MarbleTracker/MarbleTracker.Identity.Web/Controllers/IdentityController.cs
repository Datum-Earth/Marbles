using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarbleTracker.Identity.Data.Model;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarbleTracker.Identity.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public UserIdentity Get(string token)
        {
            return new UserIdentity()
            {
                User = new User()
                {
                    Id = 0,
                    UserName = "Datum_Earth",
                    Created = DateTimeOffset.UtcNow,
                    EmailAddress = "datumware@whohasthemarbles.com",
                },
                Claims = new List<Claim>()
                {
                    new Claim()
                    {
                        Id = 0,
                        Created = DateTimeOffset.UtcNow,
                        Name = "L0RD"
                    }
                }
            };
        }
    }
}
