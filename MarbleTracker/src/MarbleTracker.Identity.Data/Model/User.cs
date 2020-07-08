using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Identity.Data.Model
{
    public class User : EntityMetadata
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
    }
}
