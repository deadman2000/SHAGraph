namespace Visualizer.Model
{
    class XOR32 : IntValue
    {
        public XOR32(IntValue in1, IntValue in2)
        {
            for (int i = 0; i < 32; i++)
            {
                Bits[i] = new XOR(in1[i], in2[i]);
            }
        }
    }
}
