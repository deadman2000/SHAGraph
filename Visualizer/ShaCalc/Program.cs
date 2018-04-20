using System;
using ShaCalc.DotExport;
using ShaCalc.GraphStore;
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
            byte[] data = new byte[256];
            r.NextBytes(data);
            SHA sha = new SHA(data);

            DotBuilder builder = new DotBuilder();
            builder.OutBits = sha.OutBits();
            builder.SaveToFile(@"e:\Projects\SHA256\sha.dot");
        }

        static void BuildDot()
        {
            var a = new IntValue(32);
            var b = new IntValue(645);
            var c = a.Add(b);
            var o = new OutputInt(c);
            
            DotBuilder builder = new DotBuilder();
            builder.OutBits = Array.ConvertAll(o.GetBits(), i => (OutputBit)i);
            builder.SaveToFile(@"e:\Projects\SHA256\graph.dot");
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
            BuildDot();
        }
    }
}
