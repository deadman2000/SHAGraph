using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ShaCalc.Model;

namespace ShaCalc.Export
{
    // http://www.graphviz.org/pdf/dotguide.pdf
    // https://en.wikipedia.org/wiki/DOT_(graph_description_language)
    // https://graphviz.gitlab.io/_pages/doc/info/lang.html
    // http://www.graphviz.org/doc/info/attrs.html
    class DotBuilder
    {
        public BitValue[] OutBits;

        StringBuilder text;
        int deep = 0;

        HashSet<BitValue> allBits;

        int max_depth = 15; // For SHA 74
        int graph_depth;

        public string Build()
        {
            text = new StringBuilder();
            AppendLine("digraph {");
            deep++;

            allBits = new HashSet<BitValue>(OutBits);
            Queue<BitValue> queue = new Queue<BitValue>(OutBits);

            while (true)
            {
                if (!queue.Any())
                    break;

                var b = queue.Dequeue();

                WriteBit(b);

                int d = b.Depth + 1;

                if (d > graph_depth)
                    graph_depth = d;

                if (d > max_depth)
                    continue;

                if (b is ConstantBit)
                    continue;

                var ins = b.GetInputs();
                if (ins != null)
                    foreach (var ib in ins)
                    {
                        if (ib.Depth == 0)
                            //if (ib.Depth < d)
                            ib.Depth = d;

                        if (!allBits.Contains(ib))
                        {
                            queue.Enqueue(ib);
                            allBits.Add(ib);
                        }
                    }
            }


            Console.WriteLine("Depth: " + graph_depth);
            Console.WriteLine("Nodes: " + allBits.Count);
            foreach (var b in allBits)
            {
                if (b.Depth == max_depth) continue;

                var ins = b.GetInputs();
                if (ins == null) continue;
                foreach (var i in ins)
                {
                    if (allBits.Contains(i))
                        AppendLine("bit" + i.ID + " -> bit" + b.ID);
                }
            }

            deep--;
            AppendLine("}");
            return text.ToString();
        }

        private void WriteBit(BitValue bit)
        {
            StringBuilder sb = new StringBuilder("bit").Append(bit.ID)
                .Append(" [")
                .Append("label=\"").Append(bit.GetName()).Append("\"")
                .Append(" color=").Append(GetColor(bit))
                .Append(" depth=").Append(bit.Depth).Append("]");
            AppendLine(sb.ToString());
        }

        private static string GetColor(BitValue bit)
        {
            return "gray";
        }

        private void AppendLine(string line)
        {
            for (int i = 0; i < deep; i++)
                text.Append("  ");
            text.AppendLine(line);
        }

        public void SaveToFile(string path)
        {
            Build();
            File.WriteAllText(path, text.ToString());
        }
    }
}
