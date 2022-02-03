using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Mining.Models;
using Utf8Json;

namespace Mining
{
    public class Miner
    {
        public bool Mine(Block block, CancellationToken token = default(CancellationToken))
        {
            var target = ToTargetBytes(block.Bits);
            var rnd = new Random();
            var nonceSeed = new byte[sizeof(ulong)];
            rnd.NextBytes(nonceSeed);

            var nonce = BitConverter.ToUInt64(nonceSeed, 0);
            while (!token.IsCancellationRequested)
            {
                block.Nonce = nonce++;
                block.Timestamp = DateTime.UtcNow;
                var hash = block.ComputeId();
                Console.WriteLine(new HexString(hash).ToString());
                if (!HashCheck(hash, target)) continue;
                block.Id = new HexString(hash);

                Console.WriteLine();
                var bytes = JsonSerializer.Serialize(block);
                var json = JsonSerializer.PrettyPrint(bytes);
                Console.WriteLine($"Mined : {json}");
                return true;
            }

            return false;
        }

        public static bool HashCheck(byte[] data1, byte[] target)
        {
            if (data1.Length != 32 || target.Length != 32) return false;
            for (var i = 0; i < data1.Length; i++)
            {
                if (data1[i] < target[i]) return true;
                if (data1[i] > target[i]) return false;
            }
            return true;
        }

        public static byte[] ToTargetBytes(uint bits)
        {
            var byteLength = bits / 8;
            var bitLength = bits % 8;
            var bytes = new byte[32];
            for (var i = 0; i < bytes.Length; i++)
            {
                if (i < byteLength) bytes[i] = 0x00;
                else if (i == byteLength) bytes[i] = (byte)(Math.Pow(2, 8 - bitLength) - 1);
                else bytes[i] = 0xFF;
            }

            return bytes;
        }
    }
}