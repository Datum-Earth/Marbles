using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Core.Data.Models
{
    public class User : EntityMetadataBase
    {
        public string Username { get; set; }
        public long MarbleAmount { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}
