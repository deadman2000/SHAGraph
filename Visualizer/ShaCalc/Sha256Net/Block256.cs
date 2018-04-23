using System.Collections.Generic;
using ShaCalc.Model;

namespace ShaCalc.Sha256Net
{
    class Block256
    {
        protected IntValue[] Ints = new IntValue[8];

        public IntValue this[int index]
        {
            get { return Ints[index]; }
        }

        public Block256(params IntValue[] ints)
        {
            for (int i = 0; i < ints.Length; i++)
                Ints[i] = ints[i];
        }

        public Block256(Block256 other)
            : this(other.Ints)
        {
        }

        public BitValue[] GetOutBits()
        {
            BitValue[] bits = new BitValue[8 * 32];
            for (int i = 0; i < 8; i++)
            {
                var iv = Ints[i];
                for (int n = 0; n < 32; n++)
                {
                    bits[i * 32 + n] = iv[n];
                }
            }
            return bits;
        }
    }
}
