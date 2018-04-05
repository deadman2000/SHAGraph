namespace Visualizer.Model
{
    class ADD32 : IntValue
    {
        // https://www.electronics-tutorials.ws/combination/comb_7.html

        public ADD32(IntValue a, IntValue b)
        {
            Bits[0] = new XOR(a[0], b[0]);
            BitValue carry = new AND(a[0], b[0]);
            XOR x0;

            for (int i = 1; i < 32; i++)
            {
                Bits[i] = new XOR(x0 = new XOR(a[i], b[i]), carry);
                if (i != 31) // Не считаем остаток для последнего бита
                    carry = new OR(new AND(x0, carry), new AND(a[i], b[i]));
            }
        }
    }
}
