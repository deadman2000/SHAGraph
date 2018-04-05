using Visualizer.Model;

namespace Visualizer.Sha256Net
{
    class Ch : XOR32
    {
        // CH(x,y,z) (((x) & (y)) ^ (~(x) & (z)))
        public Ch(IntValue x, IntValue y, IntValue z)
            : base(new AND32(x, y), new AND32(new NOT32(x), z))
        {
        }
    }
}
