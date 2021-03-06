﻿namespace Visualizer.Model
{
    class ByteValue
    {
        /// <summary>
        /// Биты значения. 0 - самы младший бит, 7 - самый старший
        /// </summary>
        protected BitValue[] Bits = new BitValue[8];

        public BitValue this[int i]
        {
            get { return Bits[i]; }
        }

        public ByteValue()
        {
        }

        public ByteValue(byte value)
        {
            for (int i = 0; i < 8; i++)
                Bits[i] = new ConstantBit(((value >> i) & 1) == 1);
        }

        public ByteValue(int value)
        {
            for (int i = 0; i < 8; i++)
                Bits[i] = new ConstantBit(((value >> i) & 1) == 1);
        }

        public int Get()
        {
            int value = 0;
            for (int i = 0; i < 8; i++)
            {
                if (Bits[i].Get())
                    value |= (1 << i);
            }
            return value;
        }
        
        public ByteValue Shift(int shift)
        {
            return new Shift8(this, shift);
        }
    }
}
