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

        int max_depth = 100; // For SHA 74

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

                if (d > max_depth)
                    continue;

                var ins = b.GetInputs();
                if (ins != null)
                    foreach (var i in ins)
                    {
                        if (i.Depth == 0)
                            i.Depth = d;
                        
                        if (!allBits.Contains(i))
                        {
                            queue.Enqueue(i);
                            allBits.Add(i);
                        }
                    }
            }

            foreach (var b in allBits)
            {
                if (b.Depth == max_depth) continue;

                var ins = b.GetInputs();
                if (ins == null) continue;
                foreach (var i in ins)
                {
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
                .Append(" color=").Append(bit.GetColor())
                .Append(" depth=").Append(bit.Depth)
                .Append("]");
            AppendLine(sb.ToString());
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
