
namespace ShaCalc.Model
{
    class ConstantBit : BitValue
    {
        public bool Variable;

        public ConstantBit(bool value)
        {
            _value = value;
            _isCalc = true;
        }

        protected override bool? Calc()
        {
            return _value;
        }

        protected override bool Request(bool value, bool hard)
        {
            return _value.Value == value;
        }

        public override BitValue[] GetInputs()
        {
            return null;
        }

        public override string GetName()
        {
            return _value.HasValue ? (_value.Value ? "1" : "0") : "?";
        }
        
        protected override BitValue DoOptimize()
        {
            return this;
        }
    }
}
