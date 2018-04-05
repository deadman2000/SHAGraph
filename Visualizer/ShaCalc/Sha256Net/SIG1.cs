using ShaCalc.Model;

namespace ShaCalc.Sha256Net
{
    class SIG1 : XOR32
    {
        // SIG1(x) (ROTRIGHT(x,17) ^ ROTRIGHT(x,19) ^ ((x) >> 10))
        public SIG1(IntValue x)
            : base(new XOR32(x.ShiftCirc(17), x.ShiftCirc(19)), x.Shift(10))
        {
        }
    }
}
