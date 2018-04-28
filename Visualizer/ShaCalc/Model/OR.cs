
namespace ShaCalc.Model
{
    class OR : BitValue
    {
        private BitValue Input1, Input2;

        public OR(BitValue in1, BitValue in2)
        {
            Input1 = in1;
            Input2 = in2;
        }

        protected override bool? Calc()
        {
            var v1 = Input1.Get();
            var v2 = Input2.Get();

            if (v1.HasValue && v2.HasValue)
                return v1.Value || v2.Value;
            return null;
        }

        public override BitValue[] GetInputs()
        {
            return new[] { Input1, Input2 };
        }

        public override string GetName()
        {
            return "OR";
        }

        protected override bool Request(bool value, bool hard)
        {
            if (value)
            {
                if (Input1.SetTarget(true))
                    return true;
                if (Input2.SetTarget(true))
                    return true;
                return false;
            }
            else
            {
                return Input1.SetTarget(false, hard) && Input2.SetTarget(false, hard);
            }
        }

        protected override BitValue DoOptimize()
        {
            Input1 = Input1.Optimize();
            Input2 = Input2.Optimize();

            if (Input1 == Input2)
                return Input1;

            var v1 = Input1.Get();
            var v2 = Input2.Get();

            if (!v1.HasValue && !v2.HasValue)
                return this;

            if (!v1.HasValue)
            {
                if (v2.Value)
                    return new ConstantBit(true);
                else
                    return Input1;
            }
            else if (!v2.HasValue)
            {
                if (v1.Value)
                    return new ConstantBit(true);
                else
                    return Input2;
            }
            else
                return new ConstantBit(Get().Value);
        }
    }
}
