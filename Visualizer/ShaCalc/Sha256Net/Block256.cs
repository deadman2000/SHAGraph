﻿using ShaCalc.Model;

namespace ShaCalc.Sha256Net
{
    class Block256 : BitGroup
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

        public override BitValue[] GetBits()
        {
            return null;
        }

        public override BitGroup[] GetSubgroups()
        {
            return Ints;
        }
    }
}