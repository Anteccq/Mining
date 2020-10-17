using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;
using static Utf8Json.JsonSerializer;

namespace Mining.Models
{
    [MessagePackObject]
    public class Block
    {
        [Key(0)]
        public HexString Id { get; set; }
        [Key(1)]
        public HexString PrevBlockHash { get; set; }
        [Key(2)]
        public uint Bits { get; set; }
        [Key(3)]
        public DateTime Timestamp { get; set; }
        [Key(4)]
        public List<Transaction> Transactions { get; set; }
        [Key(5)]
        public ulong Nonce { get; set; }

        public Block Clone() => CloneUtil.Clone(this);

        public byte[] ComputeId()
        {
            var block = this.Clone();
            block.Id = null;
            var data = Serialize(block);
            return HashUtil.DoubleSHA256Hash(data);
        }
    }
}
