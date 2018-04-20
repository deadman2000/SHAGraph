
namespace ShaCalc.Model
{
    class XOR : BitValue
    {
        private BitValue Input1, Input2;

        public XOR(BitValue in1, BitValue in2)
        {
            Input1 = in1;
            Input2 = in2;
        }

        protected override bool Calc()
        {
            return Input1.Get() ^ Input2.Get();
        }

        public override BitValue[] GetInputs()
        {
            return new[] { Input1, Input2 };
        }

        public override string GetName()
        {
            return "XOR";
        }

        public override string GetColor()
        {
            return "gray";
        }
    }
}
