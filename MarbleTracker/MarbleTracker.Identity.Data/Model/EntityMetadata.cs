using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Identity.Data.Model
{
    public class EntityMetadata
    {
        public long Id { get; set; }
        public DateTimeOffset? Created { get; set; }
        public DateTimeOffset? LastModified { get; set; }
    }
}
