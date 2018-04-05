using ShaCalc.Model;

namespace ShaCalc.Sha256Net
{
    class SHARound : Block256
    {
        // https://github.com/B-Con/crypto-algorithms/blob/master/sha256.c

        static IntValue[] k = {
            new IntValue(0x428a2f98), new IntValue(0x71374491), new IntValue(0xb5c0fbcf), new IntValue(0xe9b5dba5), new IntValue(0x3956c25b), new IntValue(0x59f111f1), new IntValue(0x923f82a4), new IntValue(0xab1c5ed5),
            new IntValue(0xd807aa98), new IntValue(0x12835b01), new IntValue(0x243185be), new IntValue(0x550c7dc3), new IntValue(0x72be5d74), new IntValue(0x80deb1fe), new IntValue(0x9bdc06a7), new IntValue(0xc19bf174),
            new IntValue(0xe49b69c1), new IntValue(0xefbe4786), new IntValue(0x0fc19dc6), new IntValue(0x240ca1cc), new IntValue(0x2de92c6f), new IntValue(0x4a7484aa), new IntValue(0x5cb0a9dc), new IntValue(0x76f988da),
            new IntValue(0x983e5152), new IntValue(0xa831c66d), new IntValue(0xb00327c8), new IntValue(0xbf597fc7), new IntValue(0xc6e00bf3), new IntValue(0xd5a79147), new IntValue(0x06ca6351), new IntValue(0x14292967),
            new IntValue(0x27b70a85), new IntValue(0x2e1b2138), new IntValue(0x4d2c6dfc), new IntValue(0x53380d13), new IntValue(0x650a7354), new IntValue(0x766a0abb), new IntValue(0x81c2c92e), new IntValue(0x92722c85),
            new IntValue(0xa2bfe8a1), new IntValue(0xa81a664b), new IntValue(0xc24b8b70), new IntValue(0xc76c51a3), new IntValue(0xd192e819), new IntValue(0xd6990624), new IntValue(0xf40e3585), new IntValue(0x106aa070),
            new IntValue(0x19a4c116), new IntValue(0x1e376c08), new IntValue(0x2748774c), new IntValue(0x34b0bcb5), new IntValue(0x391c0cb3), new IntValue(0x4ed8aa4a), new IntValue(0x5b9cca4f), new IntValue(0x682e6ff3),
            new IntValue(0x748f82ee), new IntValue(0x78a5636f), new IntValue(0x84c87814), new IntValue(0x8cc70208), new IntValue(0x90befffa), new IntValue(0xa4506ceb), new IntValue(0xbef9a3f7), new IntValue(0xc67178f2)
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">16 int</param>
        public SHARound(Block256 state, IntValue[] data)
            : base(state)
        {
            IntValue a, b, c, d, e, f, g, h, t1, t2;
            int i;
            IntValue[] m = new IntValue[64];

            for (i = 0; i < 16; ++i)
                m[i] = data[i];

            for (; i < 64; ++i)
                m[i] = new SIG1(m[i - 2]).Add(m[i - 7]).Add(new SIG0(m[i - 15])).Add(m[i - 16]);

            a = Ints[0];
            b = Ints[1];
            c = Ints[2];
            d = Ints[3];
            e = Ints[4];
            f = Ints[5];
            g = Ints[6];
            h = Ints[7];

            for (i = 0; i < 64; i++)
            {
                t1 = h.Add(new EP1(e)).Add(new Ch(e, f, g)).Add(k[i]).Add(m[i]);
                t2 = new EP0(a).Add(new Maj(a, b, c));
                h = g;
                g = f;
                f = e;
                e = d.Add(t1);
                d = c;
                c = b;
                b = a;
                a = t1.Add(t2);
            }

            Ints[0] = Ints[0].Add(a);
            Ints[1] = Ints[1].Add(b);
            Ints[2] = Ints[2].Add(c);
            Ints[3] = Ints[3].Add(d);
            Ints[4] = Ints[4].Add(e);
            Ints[5] = Ints[5].Add(f);
            Ints[6] = Ints[6].Add(g);
            Ints[7] = Ints[7].Add(h);
        }
    }
}
