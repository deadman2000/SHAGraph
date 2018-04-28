
using System;

namespace ShaCalc.Model
{
    abstract class BitValue
    {
        static IdGenerator ID_GEN = new IdGenerator();
        public readonly int ID = ID_GEN.GetID();

        protected abstract bool? Calc();

        protected bool _isCalc = false;
        protected bool? _value;
        protected bool? target;

        public int Depth;

        public bool? Get()
        {
            if (_isCalc) return _value;
            _isCalc = true;
            _value = Calc();
            if (_value.HasValue)
            {
                if (target.HasValue && target.Value != _value.Value)
                    throw new Exception();
            }

            return _value;
        }

        public abstract BitValue[] GetInputs();

        public abstract string GetName();

        public static int conflicts = 0;

        public bool SetTarget(bool value, bool hard = false)
        {
            if (_value.HasValue)
            {
                if (_value.Value != value)
                {
                    if (hard)
                    {
                        Console.WriteLine("Conflict " + ID);
                        conflicts++;
                    }

                    return false;
                }
                return true;
            }

            if (target.HasValue)
            {
                if (target.Value == value)
                    return true;
                else
                {
                    if (hard)
                    {
                        conflicts++;
                        Console.WriteLine("Conflict " + ID);
                    }
                    return false;
                }
            }

            target = value;
            var r = Request(value, hard);
            if (hard)
            {
                if (!r)
                    throw new Exception();
                _value = value;
            }

            return r;
        }

        protected abstract bool Request(bool value, bool hard);

        bool _optimized = false;
        private BitValue _optimizedBy;

        protected abstract BitValue DoOptimize();

        public BitValue Optimize()
        {
            if (_optimized) return _optimizedBy;
            _optimized = true;

            var v = Get();
            if (v.HasValue)
                return _optimizedBy = new ConstantBit(v.Value);

            return _optimizedBy = DoOptimize();
        }
    }
}
