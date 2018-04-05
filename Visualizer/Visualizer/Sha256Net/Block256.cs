using Visualizer.Model;

namespace Visualizer.Sha256Net
{
    class Block256
    {
        public IntValue a;
        public IntValue b;
        public IntValue c;
        public IntValue d;
        public IntValue e;
        public IntValue f;
        public IntValue g;
        public IntValue h;

        public Block256(IntValue inA, IntValue inB, IntValue inC, IntValue inD, IntValue inE, IntValue inF, IntValue inG, IntValue inH)
        {
            a = inA;
            b = inB;
            c = inC;
            d = inD;
            e = inE;
            f = inF;
            g = inG;
            h = inH;
        }

        public Block256(Block256 other)
        {
            a = other.a;
            b = other.b;
            c = other.c;
            d = other.d;
            e = other.e;
            f = other.f;
            g = other.g;
            h = other.h;
        }
    }
}
