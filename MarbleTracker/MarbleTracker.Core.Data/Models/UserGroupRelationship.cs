using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Core.Data.Models
{
    public class UserGroupRelationship : EntityMetadataBase
    {
        public long UserId { get; set; }
        public long GroupId { get; set; }

        public virtual User User { get; set; }
        public virtual Group Group { get; set; }
    }
}
