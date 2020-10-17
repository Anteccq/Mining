using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;

namespace Mining.Models
{
    [MessagePackObject]
    public class Transaction
    {
        [Key(0)]
        public string From { get; set; }
        [Key(1)]
        public string To { get; set; }
        [Key(2)]
        public uint Amount { get; set; }
        [Key(3)]
        public string Engrave { get; set; }
    }
}
