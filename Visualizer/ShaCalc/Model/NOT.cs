
namespace ShaCalc.Model
{
    class NOT : BitValue
    {
        private BitValue _x;

        public NOT(BitValue x)
        {
            _x = x;
        }

        protected override bool Calc()
        {
            return !_x.Get();
        }

        public override BitValue[] GetInputs()
        {
            return new[] { _x };
        }

        public override string GetName()
        {
            return "NOT";
        }

        public override string GetColor()
        {
            return "gray";
        }
    }
}
