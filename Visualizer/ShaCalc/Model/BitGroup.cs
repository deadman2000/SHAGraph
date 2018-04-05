using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaCalc.Model
{
    abstract class BitGroup
    {
        static IdGenerator ID_GEN = new IdGenerator();
        public readonly int ID = ID_GEN.GetID();

        public abstract BitValue[] GetBits();

        public abstract BitGroup[] GetSubgroups();
    }
}
