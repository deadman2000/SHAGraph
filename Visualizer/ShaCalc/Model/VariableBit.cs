namespace ShaCalc.Model
{
    class VariableBit : BitValue
    {
        public override BitValue[] GetInputs()
        {
            return null;
        }

        public override string GetName()
        {
            return "?";
        }

        protected override BitValue DoOptimize()
        {
            return this;
        }

        protected override bool? Calc()
        {
            return null;
        }
        
        protected override bool Request(bool value, bool hard)
        {
            return true;
        }
    }
}