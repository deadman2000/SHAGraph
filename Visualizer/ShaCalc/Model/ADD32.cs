namespace ShaCalc.Model
{
    class ADD32 : IntValue
    {
        // https://www.electronics-tutorials.ws/combination/comb_7.html

        public ADD32(IntValue a, IntValue b)
        {
            Bits[0] = AddBit(new XOR(a[0], b[0]));
            BitValue carry = AddBit(new AND(a[0], b[0]));
            XOR x0;

            for (int i = 1; i < 32; i++)
            {
                Bits[i] = AddBit(new XOR(AddBit(x0 = new XOR(a[i], b[i])), carry));
                if (i != 31) // Не считаем остаток для последнего бита
                    carry = AddBit(new OR(AddBit(new AND(x0, carry)), AddBit(new AND(a[i], b[i]))));
            }
        }
    }
}
