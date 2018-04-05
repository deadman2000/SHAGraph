namespace ShaCalc.Model
{
    /// <summary>
    /// Циклично сдвигает биты вправо. 11100101 >> 1 = 01110010
    /// </summary>
    class CircularShift32 : IntValue
    {
        public CircularShift32(IntValue value, int shift)
        {
            for (int i = 0; i < 32; i++)
            {
                int s = i + shift;
                if (s > 31) s -= 32;
                Bits[i] = value[s];
            }
        }
    }
}
