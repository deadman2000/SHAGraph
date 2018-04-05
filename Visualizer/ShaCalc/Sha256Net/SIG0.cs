using ShaCalc.Model;

namespace ShaCalc.Sha256Net
{
    class SIG0 : XOR32
    {
        // SIG0(x) (ROTRIGHT(x,7) ^ ROTRIGHT(x,18) ^ ((x) >> 3))
        public SIG0(IntValue x)
            : base(new XOR32(x.ShiftCirc(7), x.ShiftCirc(18)), x.Shift(3))
        {
        }
    }
}
