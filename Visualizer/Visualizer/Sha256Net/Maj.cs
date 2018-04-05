using Visualizer.Model;

namespace Visualizer.Sha256Net
{
    class Maj : XOR32
    {
        // MAJ(x,y,z) (((x) & (y)) ^ ((x) & (z)) ^ ((y) & (z)))
        public Maj(IntValue x, IntValue y, IntValue z)
            : base(new XOR32(new AND32(x, y), new AND32(x, z)), new AND32(y, z))
        {
        }
    }
}
