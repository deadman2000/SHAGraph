using System;
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
            BitValue[] bits = new BitValue[Ints.Length * 32];
            for (int i = 0; i < Ints.Length; i++)
            {
                var iv = Ints[i];
                for (int n = 0; n < 4; n++)
                    for (int b = 0; b < 8; b++)
                    {
                        bits[i * 32 + n * 8 + b] = iv[(3 - n) * 8 + b];
                    }
            }
            return bits;
        }

        public byte[] ToBytes()
        {
            byte[] value = new byte[Ints.Length * 4];
            for (int i = 0; i < Ints.Length; i++)
            {
                uint v = Ints[i].Get();
                value[i * 4 + 3] = (byte)(v);
                value[i * 4 + 2] = (byte)(v >> 8);
                value[i * 4 + 1] = (byte)(v >> 16);
                value[i * 4] = (byte)(v >> 24);
            }
            return value;
        }
    }
}
