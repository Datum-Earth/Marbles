using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Core.Data.Models
{
    public class ChallengeResult : EntityMetadataBase
    {
        public ChallengeResultStatus Result { get; set; }
        public long WitnessId { get; set; }
        
        public virtual User Witness { get; set; }
    }
}
