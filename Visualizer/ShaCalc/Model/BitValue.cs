using System.Drawing;
using ShaCalc.Rendering;

namespace ShaCalc.Model
{
    abstract class BitValue : Drawable
    {
        static IdGenerator ID_GEN = new IdGenerator();
        public readonly int ID = ID_GEN.GetID();

        protected abstract bool Calc();

        protected bool _isCalc = false;
        protected bool _value;

        public BitValue()
        {
        }

        public bool Get()
        {
            if (_isCalc) return _value;
            _isCalc = true;
            return _value = Calc();
        }

        protected override Color GetOutColor()
        {
            if (!_isCalc) return Color.Gray;
            if (_value) return Color.Red;
            return Color.Black;
        }

        public override Drawable GetInput(int n)
        {
            return GetInputs()[n];
        }

        public abstract BitValue[] GetInputs();

        public abstract string GetName();

        public abstract string GetColor();
    }
}
