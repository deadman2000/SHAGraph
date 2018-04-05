using System;
using ShaCalc.DotExport;
using ShaCalc.Model;
using ShaCalc.Rendering;
using ShaCalc.Sha256Net;

namespace ShaCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Random r = new Random();

            byte[] data = new byte[256];
            r.NextBytes(data);
            //byte[] data = new byte[0];

            SHA mySHA = new SHA(data);
            mySHA.ResultStr();*/

            /*ConstantBit a = new ConstantBit(false);
            ConstantBit b = new ConstantBit(true);
            var xor = new XOR(a, b);
            OutputBit o = new OutputBit(xor);*/

            /*Visualizer v = new Visualizer();
            v.Drawable.Add(a);
            v.Drawable.Add(b);
            v.AddOut(o);
            v.Organize();
            v.Start();*/

            DotBuilder builder = new DotBuilder();
            //builder.Add(a, b, xor, o);

            var a = new IntValue(32);
            var b = new IntValue(645);
            var c = a.Add(b);
            var o = new OutputInt(c);

            builder.Add(a, b, c, o);

            builder.SaveToFile(@"e:\Projects\SHA256\graph.txt");
        }
    }
}
