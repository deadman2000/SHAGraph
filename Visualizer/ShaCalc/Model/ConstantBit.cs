using ShaCalc.Rendering;
using System;

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

        static BlockStyle Style0 = new ConstantBitStyle(false);
        static BlockStyle Style1 = new ConstantBitStyle(true);

        public override BlockStyle GetStyle()
        {
            if (_value)
                return Style1;
            return Style0;
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
    }
}
