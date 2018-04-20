
namespace ShaCalc.Model
{
    class ConstantBit : BitValue
    {
        public ConstantBit(bool value)
        {
            _value = value;
            _isCalc = true;
        }

        protected override bool Calc()
        {
            return _value;
        }

        public override BitValue[] GetInputs()
        {
            return null;
        }

        public override string GetName()
        {
            if (_value) return "1";
            return "0";
        }

        public override string GetColor()
        {
            return "red";
        }
    }
}
