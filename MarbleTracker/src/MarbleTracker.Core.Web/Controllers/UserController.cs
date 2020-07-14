using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using MarbleTracker.Core.Data;
using MarbleTracker.Core.Data.Models;
using MarbleTracker.Core.Service.DataService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MarbleTracker.Core.Web.Controllers
{
    [Route("api/user/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public async Task Post([FromQuery] string username)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MarbleContext>();
            optionsBuilder.UseSqlite("Data Source=data.db");

            var svc = new ServiceCore(optionsBuilder.Options, User.Identity.Name);

            await svc.CreateUser(username);
        }

        [HttpGet]
        [Route("")]
        public async Task<User> Get([FromQuery] string username)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MarbleContext>();
            optionsBuilder.UseSqlite("Data Source=data.db");

            var svc = new ServiceCore(optionsBuilder.Options, User.Identity.Name);

            return await svc.GetUser(username);
        }
    }
}
