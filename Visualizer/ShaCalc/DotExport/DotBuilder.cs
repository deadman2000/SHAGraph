using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShaCalc.Model;

namespace ShaCalc.DotExport
{
    // http://www.graphviz.org/pdf/dotguide.pdf
    // https://en.wikipedia.org/wiki/DOT_(graph_description_language)
    // https://graphviz.gitlab.io/_pages/doc/info/lang.html
    // http://www.graphviz.org/doc/info/attrs.html
    class DotBuilder
    {
        private List<BitValue> _bits = new List<BitValue>();
        private List<BitGroup> _groups = new List<BitGroup>();

        private List<BitValue> _allBits;

        StringBuilder text;
        int deep = 0;

        public string Build()
        {
            _allBits = new List<BitValue>();

            text = new StringBuilder();
            AppendLine("digraph {");
            deep++;

            _allBits.AddRange(_bits);
            foreach (var b in _bits)
                WriteBit(b);

            foreach (var g in _groups)
                WriteGroup(g);

            foreach (var b in _allBits)
            {
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
                .Append(" [label=\"").Append(bit.GetName()).Append("\"")
                .Append(" color=").Append(bit.GetColor()).Append("]");
            AppendLine(sb.ToString());
        }

        private void WriteGroup(BitGroup group)
        {
            AppendLine("subgraph cluster_" + group.ID + " {");
            deep++;

            var bits = group.GetBits();
            if (bits != null)
            {
                _allBits.AddRange(bits);
                foreach (var b in bits)
                    WriteBit(b);
            }

            var sg = group.GetSubgroups();
            if (sg != null)
            {
                foreach (var g in sg)
                    WriteGroup(g);
            }

            deep--;
            AppendLine("}");
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

        public void Add(BitValue bit)
        {
            _bits.Add(bit);
        }

        public void Add(params BitValue[] bits)
        {
            _bits.AddRange(bits);
        }

        public void Add(BitGroup group)
        {
            _groups.Add(group);
        }

        public void Add(params BitGroup[] groups)
        {
            _groups.AddRange(groups);
        }
    }
}
