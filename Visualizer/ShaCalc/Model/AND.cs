
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

        protected override bool? Calc()
        {
            var v1 = Input1.Get();
            var v2 = Input2.Get();

            if (v1.HasValue && v2.HasValue)
                return v1.Value && v2.Value;
            return null;
        }

        protected override bool Request(bool value, bool hard)
        {
            if (value)
            {
                if (Input1.SetTarget(true, hard) && Input2.SetTarget(true, hard))
                    return true;
                return false;
            }
            else
            {
                if (Input1.SetTarget(false))
                    return true;
                if (Input2.SetTarget(false))
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
            return "AND";
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
                    return Input1;
                else
                    return new NOT(Input1).Optimize();
            }
            else if (!v2.HasValue)
            {
                if (v1.Value)
                    return Input2;
                else
                    return new NOT(Input2).Optimize();
            }
            return this;
        }
    }
}
