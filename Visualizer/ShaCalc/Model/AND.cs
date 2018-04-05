using ShaCalc.Rendering;
using System;

namespace ShaCalc.Model
{
    class AND : BitValue
    {
        private BitValue Input1, Input2;

        public AND(BitValue in1, BitValue in2)
        {
            Input1 = in1;
            Input2 = in2;
        }

        protected override bool Calc()
        {
            return Input1.Get() && Input2.Get();
        }
        
        static BlockStyle Style = new BitStyle("AND", 2);
        public override BlockStyle GetStyle()
        {
            return Style;
        }
        public override BitValue[] GetInputs()
        {
            return new[] { Input1, Input2 };
        }

        public override string GetName()
        {
            return "AND";
        }
    }
}
