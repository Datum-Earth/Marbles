using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Core.Data.Models
{
    public class Transaction : EntityMetadataBase
    {
        public decimal TransferAmount { get; set; }
        public decimal SenderBalance { get; set; }
        public decimal RecieverBalance { get; set; }
        
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
        
        public virtual User Sender { get; set; }
        public virtual User Reciever { get; set; }
    }
}
