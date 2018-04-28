
namespace ShaCalc.Model
{
    class XOR : BitValue
    {
        private BitValue Input1, Input2;

        public XOR(BitValue in1, BitValue in2)
        {
            Input1 = in1;
            Input2 = in2;
        }

        protected override bool? Calc()
        {
            var v1 = Input1.Get();
            var v2 = Input2.Get();

            if (v1.HasValue && v2.HasValue)
                return v1.Value ^ v2.Value;
            return null;
        }

        protected override bool Request(bool value, bool hard)
        {
            if (value)
            {
                if (Input1.SetTarget(true) && Input2.SetTarget(false))
                    return true;
                if (Input1.SetTarget(false) && Input2.SetTarget(true))
                    return true;
                return false;
            }
            else
            {
                if (Input1.SetTarget(true) && Input2.SetTarget(true))
                    return true;
                if (Input1.SetTarget(false) && Input2.SetTarget(false))
                    return true;
                return false;
            }
        }

        public override BitValue[] GetInputs()
        {
            return new[] { Input1, Input2 };
        }

        public override string GetName()
        {
            return "XOR";
        }

        protected override BitValue DoOptimize()
        {
            Input1 = Input1.Optimize();
            Input2 = Input2.Optimize();

            if (Input1 == Input2)
                return new ConstantBit(false);

            var v1 = Input1.Get();
            var v2 = Input2.Get();

            if (!v1.HasValue && !v2.HasValue)
                return this;

            if (!v1.HasValue)
            {
                if (v2.Value)
                    return new NOT(Input1).Optimize();
                else
                    return Input1;
            }
            else if (!v2.HasValue)
            {
                if (v1.Value)
                    return new NOT(Input2).Optimize();
                else
                    return Input2;
            }
            return this;
        }
    }
}
