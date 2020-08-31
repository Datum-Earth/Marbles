using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Core.Data.Models
{
    public class Challenge : EntityMetadataBase
    {
        public ChallengeType Type { get; set; }
        public ChallengeStatus Status { get; set; }
        public ChallengeResult Result { get; set; }
        
        public virtual Group SourceGroup { get; set; }
        public virtual Group TargetGroup { get; set; }
        public virtual User Witness { get; set; }
    }
}
