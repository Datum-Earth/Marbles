using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Identity.Data.Model
{
    public class UserIdentity
    {
        public User User { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }
}
