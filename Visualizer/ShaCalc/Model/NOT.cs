
namespace ShaCalc.Model
{
    class NOT : BitValue
    {
        private BitValue _x;

        public NOT(BitValue x)
        {
            _x = x;
        }

        protected override bool? Calc()
        {
            var v = _x.Get();
            if (v.HasValue)
                return !v;
            return null;
        }

        protected override bool Request(bool value, bool hard)
        {
            if (_x.SetTarget(!value, hard))
            {
                _value = value;
                return true;
            }
            return false;
        }

        public override BitValue[] GetInputs()
        {
            return new[] { _x };
        }

        public override string GetName()
        {
            return "NOT";
        }

        protected override BitValue DoOptimize()
        {
            _x = _x.Optimize();

            if (_x is NOT)
                return ((NOT)_x)._x;

            return this;
        }
    }
}
