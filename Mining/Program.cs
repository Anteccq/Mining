using System;
using System.Collections.Generic;
using System.Threading;
using Mining.Models;
using Utf8Json;
using static Utf8Json.JsonSerializer;

namespace Mining
{
    class Program
    {
        private static uint _bits = 18;
        private static List<Block> Chain { get; } = new List<Block>();

        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, __) =>
            {
                cts.Cancel();
                cts.Dispose();
                cts = null;
            };

            //First Block Mining
            var tx = new Transaction()
            {
                Amount = 10,
                From = "Alice",
                To = "Anteccq",
                Engrave = "絵画購入のため"
            };
            var tx2 = new Transaction()
            {
                Amount = 3,
                From = "A",
                To = "B",
                Engrave = "Sample"
            };

            var block = new Block()
            {
                PrevBlockHash = null,
                Transactions = new List<Transaction>() { tx, tx2 },
                Bits = _bits
            };
            var miner = new Miner();
            if (!miner.Mine(block, cts.Token)) return;
            Chain.Add(block);

            Console.WriteLine("キーを押すと次のステップが始まります。 1/2");
            Console.ReadKey();

            //Second Block Mining
            var tx3 = new Transaction()
            {
                Amount = 1,
                From = "Anteccq",
                To = "Engineers",
                Engrave = "祝 LT 会"
            };
            var block2 = new Block()
            {
                PrevBlockHash = block.Id,
                Transactions = new List<Transaction>(){tx3},
                Bits = _bits
            };
            if(miner.Mine(block2)) return;
            Chain.Add(block2);
            Console.WriteLine(PrettyPrint(Serialize(Chain)));
            Console.WriteLine("実行終了 2/2");
        }
    }
}
