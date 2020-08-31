using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Core.Data.Models
{
    public class Wager : EntityMetadataBase
    {
        public decimal Amount { get; set; }

        public virtual User User { get; set; }
        public virtual Challenge Challenge { get; set; }
    }
}
