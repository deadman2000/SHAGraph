
namespace ShaCalc.Model
{
    abstract class BitValue
    {
        static IdGenerator ID_GEN = new IdGenerator();
        public readonly int ID = ID_GEN.GetID();

        protected abstract bool Calc();

        protected bool _isCalc = false;
        protected bool _value;

        public int Depth;
        
        public bool Get()
        {
            if (_isCalc) return _value;
            _isCalc = true;
            return _value = Calc();
        }

        public abstract BitValue[] GetInputs();

        public abstract string GetName();

        public abstract string GetColor();
    }
}
