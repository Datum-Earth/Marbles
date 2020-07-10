using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Core.Data.Models
{
    public class Challenge : EntityMetadataBase
    {
        public ChallengeType Type { get; set; }

        public long SourceGroupId { get; set; }
        public long TargetGroupId { get; set; }

        public Group SourceGroup { get; set; }
        public Group TargetGroup { get; set; }
    }
}
