using System;
using ShaCalc.DotExport;
using ShaCalc.GraphStore;
using ShaCalc.Model;
using ShaCalc.Rendering;
using ShaCalc.Sha256Net;

namespace ShaCalc
{
    class Program
    {
        // https://www.researchgate.net/post/How_can_I_visualize_a_big_Neo4j_dataset

        static void BuildDotSHA()
        {
            Random r = new Random();
            byte[] data = new byte[256];
            r.NextBytes(data);
            SHA sha = new SHA(data);

            DotBuilder builder = new DotBuilder();
            builder.Add(sha);
            builder.SaveToFile(@"e:\Projects\SHA256\sha.dot");
        }

        static void BuildDot()
        {
            var a = new IntValue(32);
            var b = new IntValue(645);
            var c = a.Add(b);
            var o = new OutputInt(c);
            
            DotBuilder builder = new DotBuilder();
            builder.Add(a, b, c, o);
            builder.SaveToFile(@"e:\Projects\SHA256\graph.dot");
        }

        static void Visualize()
        {
            /*Random r = new Random();

            byte[] data = new byte[256];
            r.NextBytes(data);
            //byte[] data = new byte[0];

            SHA mySHA = new SHA(data);
            mySHA.ResultStr();*/

            ConstantBit a = new ConstantBit(false);
            ConstantBit b = new ConstantBit(true);
            var xor = new XOR(a, b);
            OutputBit o = new OutputBit(xor);

            Visualizer v = new Visualizer();
            v.Drawable.Add(a);
            v.Drawable.Add(b);
            v.AddOut(o);
            v.Organize();
            v.Start();
        }

        static void Store()
        {
            /*var a = new IntValue(32);
            var b = new IntValue(645);
            var c = a.Add(b);
            var o = new OutputInt(c);*/

            Random r = new Random();
            byte[] data = new byte[256];
            r.NextBytes(data);
            SHA sha = new SHA(data);

            GraphStorer storer = new GraphStorer();
            //storer.Add(a, b, c, o);
            storer.Add(sha);
            storer.Save();
        }

        static void Main(string[] args)
        {
            BuildDotSHA();
        }
    }
}
