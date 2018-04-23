using System;
using ShaCalc.Export;
using ShaCalc.Model;
using ShaCalc.Sha256Net;

namespace ShaCalc
{
    class Program
    {
        // https://www.researchgate.net/post/How_can_I_visualize_a_big_Neo4j_dataset

        static void BuildDotSHA()
        {
            Random r = new Random();
            byte[] data = new byte[63];
            r.NextBytes(data);
            SHA sha = new SHA(data);

            DotBuilder builder = new DotBuilder();
            builder.OutBits = sha.OutBits();
            builder.SaveToFile(@"..\..\..\..\sha.dot");
        }

        static void BuildDot()
        {
            var a = new IntValue(32);
            var b = new IntValue(645);
            var c = a.Add(b);
            var o = new OutputInt(c);
            
            DotBuilder builder = new DotBuilder();
            builder.OutBits = Array.ConvertAll(o.Bits, i => (OutputBit)i);
            builder.SaveToFile(@"..\..\..\..\graph.dot");
        }

        static void CalcSHA()
        {
            byte[] data = new byte[1] { 0xe3 };
            SHA sha = new SHA(data);
            sha.ResultStr();
            Console.ReadLine();
        }
        
        static void Main(string[] args)
        {
            BuildDotSHA();
        }
    }
}
