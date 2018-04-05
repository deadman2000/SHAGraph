namespace ShaCalc.Model
{
    class Shift32 : IntValue
    {
        /// <summary>
        /// Сдвигает вправо при положительном shift. Влево при отрицательномм
        /// </summary>
        /// <param name="value"></param>
        /// <param name="shift"></param>
        public Shift32(IntValue value, int shift)
        {
            for (int i = 0; i < 32; i++)
            {
                int s = i + shift;
                if (s >= 32)
                    Bits[i] = new ConstantBit(false);
                else
                    Bits[i] = value[s];
            }
        }
    }
}