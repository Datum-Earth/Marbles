using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Identity.Data.Model
{
    public class UserClaim : EntityMetadata
    {
        public User User { get; set; }
        public Claim Claim { get; set; }
        public bool IsActive { get; set; }
    }
}
