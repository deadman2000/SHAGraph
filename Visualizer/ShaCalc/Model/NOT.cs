using ShaCalc.Rendering;
using System;

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

        static BlockStyle Style = new BitStyle("NOT", 1);
        public override BlockStyle GetStyle()
        {
            return Style;
        }

        public override BitValue[] GetInputs()
        {
            return new[] { _x };
        }

        public override string GetName()
        {
            return "NOT";
        }
    }
}
