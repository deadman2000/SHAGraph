using ShaCalc.Rendering;
using System;
using System.Drawing;

namespace ShaCalc.Model
{
    class OutputBit : BitValue
    {
        private BitValue _input;

        public OutputBit(BitValue input)
        {
            _input = input;
        }

        public override Drawable GetInput(int n)
        {
            switch (n)
            {
                case 0: return _input;
                default: throw new Exception();
            }
        }

        static BlockStyle Style0 = new OutputBitStyle(false);
        static BlockStyle Style1 = new OutputBitStyle(true);

        public override BlockStyle GetStyle()
        {
            if (_input.Get())
                return Style1;
            return Style0;
        }

        protected override Color GetOutColor()
        {
            return Color.Black;
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
    }
}
