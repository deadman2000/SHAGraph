namespace Visualizer.Model
{
    class NOT : BitValue
    {
        private BitValue _x;

        public NOT(BitValue x)
        {
            _x = x;
        }

        public override bool Get()
        {
            return !_x.Get();
        }
    }
}
