
namespace ShaCalc.Model
{
    class OutputBit : BitValue
    {
        private BitValue _input;

        public OutputBit(BitValue input)
        {
            _input = input;
        }

        protected override bool Calc()
        {
            return _input.Get();
        }

        public override BitValue[] GetInputs()
        {
            return new[] { _input };
        }

        public override string GetName()
        {
            if (Get()) return "1";
            return "0";
        }

        public override string GetColor()
        {
            return "blue";
        }
    }
}
