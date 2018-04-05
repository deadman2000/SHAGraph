using ShaCalc.Model;

namespace ShaCalc.Sha256Net
{
    class EP0 : XOR32
    {
        // EP0(x) (ROTRIGHT(x,2) ^ ROTRIGHT(x,13) ^ ROTRIGHT(x,22))
        public EP0(IntValue x)
            : base(new XOR32(x.ShiftCirc(2), x.ShiftCirc(13)), x.ShiftCirc(22))
        {
        }
    }
}
