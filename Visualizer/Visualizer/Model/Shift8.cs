namespace Visualizer.Model
{
    class Shift8 : ByteValue
    {
        /// <summary>
        /// Сдвигает вправо при положительном shift. Влево при отрицательномм
        /// </summary>
        /// <param name="value"></param>
        /// <param name="shift"></param>
        public Shift8(ByteValue value, int shift)
        {
            for (int i = 0; i < 8; i++)
            {
                int s = i + shift;
                if (s >= 8)
                    Bits[i] = new ConstantBit(false);
                else
                    Bits[i] = value[s];
            }
        }
    }
}