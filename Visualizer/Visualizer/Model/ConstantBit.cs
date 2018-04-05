namespace Visualizer.Model
{
    class ConstantBit : BitValue
    {
        private bool _value;

        public ConstantBit(bool value)
        {
            _value = value;
        }

        public override bool Get()
        {
            return _value;
        }
    }
}