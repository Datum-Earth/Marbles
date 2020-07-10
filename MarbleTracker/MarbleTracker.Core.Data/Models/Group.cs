using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Core.Data.Models
{
    public class Group : EntityMetadataBase
    {
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
