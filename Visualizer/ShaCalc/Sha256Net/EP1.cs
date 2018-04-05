using ShaCalc.Model;

namespace ShaCalc.Sha256Net
{
    class EP1 : XOR32
    {
        // EP1(x) (ROTRIGHT(x,6) ^ ROTRIGHT(x,11) ^ ROTRIGHT(x,25))
        public EP1(IntValue x)
            : base(new XOR32(x.ShiftCirc(6), x.ShiftCirc(11)), x.ShiftCirc(25))
        {
        }
    }
}
