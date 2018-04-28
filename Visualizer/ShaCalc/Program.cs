using System;
using System.Collections.Generic;
using ShaCalc.Export;
using ShaCalc.Model;
using ShaCalc.Sha256Net;

namespace ShaCalc
{
    class Program
    {
        static void BuildDotSHA()
        {
            byte[] data = new byte[63];
            SHA sha = new SHA(data);

            DotBuilder builder = new DotBuilder();
            builder.OutBits = sha.OutBits();
            builder.SaveToFile(@"..\..\..\..\sha.dot");
        }

        static void BuildBitcoinSHA()
        {
            ByteValue[] data = new ByteValue[80];
            for (int i = 0; i < 76; i++)
            {
                data[i] = new ByteValue(0);
            }
            for (int i = 76; i < 80; i++)
            {
                data[i] = new ByteValue();
            }

            var outbits = new SHA(data).OutBits();
            outbits = new SHA(outbits).OutBits();

            for (int i = 0; i < outbits.Length; i++)
            {
                outbits[i] = outbits[i].Optimize();
            }

            List<BitValue> targetBits = new List<BitValue>();
            for (int i = 0; i < 32; i++) // Last 4 bytes
            {
                var b = outbits[outbits.Length - 32 + i];
                //bool t = b.SetTarget(false, true);
                //Console.WriteLine(i + " " + t);
                targetBits.Add(b);
            }

            targetBits[0].SetTarget(false, true);

            Console.WriteLine("Conflicts: " + BitValue.conflicts);

            DotBuilder builder = new DotBuilder();
            builder.OutBits = new BitValue[] { targetBits[0] };
            //builder.OutBits = targetBits.ToArray();
            //builder.OutBits = outbits;
            builder.SaveToFile(@"..\..\..\..\sha.dot");
        }

        static void BuildDot()
        {
            var a = new IntValue(32);
            var b = new IntValue(645);
            var c = a.Add(b);

            DotBuilder builder = new DotBuilder();
            builder.OutBits = c.Bits;
            builder.SaveToFile(@"..\..\..\..\graph.dot");
        }

        static void CheckSHA()
        {
            byte[] data = new byte[1] { 0xe3 };
            SHA sha = new SHA(data);
        }

        static void Main(string[] args)
        {
            BuildBitcoinSHA();

            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
