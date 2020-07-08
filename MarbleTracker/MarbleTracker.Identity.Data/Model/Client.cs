using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Identity.Data.Model
{
    public class Client : EntityMetadata
    {
        public string Name { get; set; }
        public byte[] Secret { get; set; }
    }
}
