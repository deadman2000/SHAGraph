namespace Visualizer.Model
{
    class XOR : BitValue
    {
        private BitValue Input1, Input2;

        public XOR(BitValue in1, BitValue in2)
        {
            Input1 = in1;
            Input2 = in2;
        }

        public override bool Get()
        {
            return Input1.Get() ^ Input2.Get();
        }
    }
}
