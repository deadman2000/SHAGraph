using System;
using System.Collections.Generic;
using System.Linq;
using Neo4j.Driver.V1;
using ShaCalc.Model;

namespace ShaCalc.GraphStore
{
    // https://neo4j.com/developer/dotnet/

    class GraphStorer : IDisposable
    {
        private readonly IDriver _driver;

        public GraphStorer()
        {
            _driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "12345678"));
        }

        public void Dispose()
        {
            _driver?.Dispose();
        }

        private List<BitValue> _bits = new List<BitValue>();
        private List<BitGroup> _groups = new List<BitGroup>();
        List<BitValue> _allBits = new List<BitValue>();

        public void Add(params BitValue[] bits)
        {
            _bits.AddRange(bits);
        }

        public void Add(params BitGroup[] groups)
        {
            _groups.AddRange(groups);
        }

        private ISession session;

        public void Save()
        {
            _allBits.Clear();

            _allBits.AddRange(_bits);

            using (session = _driver.Session())
            {
                session.WriteTransaction(tx =>
                {
                    tx.Run("MATCH(n) DETACH DELETE n");
                });

                foreach (var b in _bits)
                    WriteBit(b);

                foreach (var g in _groups)
                    WriteGroup(g);

                foreach (var b in _allBits)
                {
                    Console.WriteLine("-" + b.ID);

                    var ins = b.GetInputs();
                    if (ins == null) continue;
                    foreach (var i in ins)
                    {
                        session.WriteTransaction(tx =>
                        {
                            tx.Run("MATCH (a),(b) WHERE a.id=$a AND b.id=$b CREATE (a)-[:OUT]->(b)",
                                new { a = i.ID, b = b.ID });
                        });
                    }
                }
            }
            session = null;
        }


        private void WriteGroup(BitGroup group)
        {
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
        }

        private void WriteBit(BitValue bit)
        {
            Console.WriteLine(bit.ID);
            session.WriteTransaction(tx =>
            {
                tx.Run("CREATE (b:" + bit.GetType().Name + " { id: $id, name: $label})",
                    new { id = bit.ID, label = bit.GetName() });
            });
        }

    }
}
