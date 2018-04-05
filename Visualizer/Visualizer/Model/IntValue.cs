namespace Visualizer.Model
{
    class IntValue
    {
        /// <summary>
        /// Биты значения. 0 - самы младший бит, 31 - самый старший
        /// </summary>
        protected BitValue[] Bits = new BitValue[32];

        public BitValue this[int i]
        {
            get { return Bits[i]; }
        }

        public IntValue()
        {
        }

        public IntValue(int value)
        {
            for (int i = 0; i < 32; i++)
                Bits[i] = new ConstantBit(((value >> i) & 1) == 1);
        }

        public IntValue(uint value)
        {
            for (int i = 0; i < 32; i++)
                Bits[i] = new ConstantBit(((value >> i) & 1) == 1);
        }

        public IntValue(ByteValue b0, ByteValue b1, ByteValue b2, ByteValue b3)
        {
            for (int i = 0; i < 8; i++)
                Bits[i] = b0[i];
            for (int i = 0; i < 8; i++)
                Bits[i + 8] = b1[i];
            for (int i = 0; i < 8; i++)
                Bits[i + 16] = b2[i];
            for (int i = 0; i < 8; i++)
                Bits[i + 24] = b3[i];
        }

        public int Get()
        {
            int value = 0;
            for (int i = 0; i < 32; i++)
            {
                if (Bits[i].Get())
                    value |= (1 << i);
            }
            return value;
        }

        /// <summary>
        /// Сдвигает вправо при положительном shift. Влево при отрицательномм
        /// </summary>
        /// <param name="shift"></param>
        /// <returns></returns>
        public IntValue ShiftCirc(int shift)
        {
            return new CircularShift32(this, shift);
        }

        /// <summary>
        /// Сдвигает вправо при положительном shift. Влево при отрицательномм
        /// </summary>
        /// <param name="shift"></param>
        /// <returns></returns>
        public IntValue Shift(int shift)
        {
            return new Shift32(this, shift);
        }

        public IntValue Add(IntValue arg)
        {
            return new ADD32(this, arg);
        }
    }
}
