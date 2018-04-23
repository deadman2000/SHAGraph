#include "sha.h"

#include <QDebug>

// CH(x,y,z) (((x) & (y)) ^ (~(x) & (z)))
IntValue Ch(IntValue &x, IntValue &y, IntValue &z)
{
    return x.And(y).Xor(x.Not().And(z));
}

// EP0(x) (ROTRIGHT(x,2) ^ ROTRIGHT(x,13) ^ ROTRIGHT(x,22))
IntValue EP0(IntValue & x)
{
    return x.ShiftCirc(2).Xor(x.ShiftCirc(13)).Xor(x.ShiftCirc(22));
}

// EP1(x) (ROTRIGHT(x,6) ^ ROTRIGHT(x,11) ^ ROTRIGHT(x,25))
IntValue EP1(IntValue & x)
{
    return x.ShiftCirc(6).Xor(x.ShiftCirc(11)).Xor(x.ShiftCirc(25));
}

// MAJ(x,y,z) (((x) & (y)) ^ ((x) & (z)) ^ ((y) & (z)))
IntValue Maj(IntValue & x, IntValue & y, IntValue & z)
{
    return x.And(y).Xor(x.And(z)).Xor(y.And(z));
}

// SIG0(x) (ROTRIGHT(x,7) ^ ROTRIGHT(x,18) ^ ((x) >> 3))
IntValue SIG0(IntValue & x)
{
    return x.ShiftCirc(7).Xor(x.ShiftCirc(18)).Xor(x.Shift(3));
}

// SIG1(x) (ROTRIGHT(x,17) ^ ROTRIGHT(x,19) ^ ((x) >> 10))
IntValue SIG1(IntValue & x)
{
    return x.ShiftCirc(17).Xor(x.ShiftCirc(19)).Xor(x.Shift(10));
}

static IntValue k[] = {
    IntValue(0x428a2f98), IntValue(0x71374491), IntValue(0xb5c0fbcf), IntValue(0xe9b5dba5), IntValue(0x3956c25b), IntValue(0x59f111f1), IntValue(0x923f82a4), IntValue(0xab1c5ed5),
    IntValue(0xd807aa98), IntValue(0x12835b01), IntValue(0x243185be), IntValue(0x550c7dc3), IntValue(0x72be5d74), IntValue(0x80deb1fe), IntValue(0x9bdc06a7), IntValue(0xc19bf174),
    IntValue(0xe49b69c1), IntValue(0xefbe4786), IntValue(0x0fc19dc6), IntValue(0x240ca1cc), IntValue(0x2de92c6f), IntValue(0x4a7484aa), IntValue(0x5cb0a9dc), IntValue(0x76f988da),
    IntValue(0x983e5152), IntValue(0xa831c66d), IntValue(0xb00327c8), IntValue(0xbf597fc7), IntValue(0xc6e00bf3), IntValue(0xd5a79147), IntValue(0x06ca6351), IntValue(0x14292967),
    IntValue(0x27b70a85), IntValue(0x2e1b2138), IntValue(0x4d2c6dfc), IntValue(0x53380d13), IntValue(0x650a7354), IntValue(0x766a0abb), IntValue(0x81c2c92e), IntValue(0x92722c85),
    IntValue(0xa2bfe8a1), IntValue(0xa81a664b), IntValue(0xc24b8b70), IntValue(0xc76c51a3), IntValue(0xd192e819), IntValue(0xd6990624), IntValue(0xf40e3585), IntValue(0x106aa070),
    IntValue(0x19a4c116), IntValue(0x1e376c08), IntValue(0x2748774c), IntValue(0x34b0bcb5), IntValue(0x391c0cb3), IntValue(0x4ed8aa4a), IntValue(0x5b9cca4f), IntValue(0x682e6ff3),
    IntValue(0x748f82ee), IntValue(0x78a5636f), IntValue(0x84c87814), IntValue(0x8cc70208), IntValue(0x90befffa), IntValue(0xa4506ceb), IntValue(0xbef9a3f7), IntValue(0xc67178f2)
};

Block256 SHARound(Block256 &state, IntValue *data)
{
    Block256 res(state);

    IntValue a, b, c, d, e, f, g, h, t1, t2;
    int i;
    IntValue m[64];

    for (i = 0; i < 16; ++i)
        m[i] = data[i];

    for (; i < 64; ++i)
        m[i] = SIG1(m[i - 2]).Add(m[i - 7]).Add(SIG0(m[i - 15])).Add(m[i - 16]);

    a = res.Ints[0];
    b = res.Ints[1];
    c = res.Ints[2];
    d = res.Ints[3];
    e = res.Ints[4];
    f = res.Ints[5];
    g = res.Ints[6];
    h = res.Ints[7];

    for (i = 0; i < 64; i++)
    {
        t1 = h.Add(EP1(e)).Add(Ch(e, f, g)).Add(k[i]).Add(m[i]);
        t2 = EP0(a).Add(Maj(a, b, c));
        h = g;
        g = f;
        f = e;
        e = d.Add(t1);
        d = c;
        c = b;
        b = a;
        a = t1.Add(t2);
    }

    res.Ints[0] = res.Ints[0].Add(a);
    res.Ints[1] = res.Ints[1].Add(b);
    res.Ints[2] = res.Ints[2].Add(c);
    res.Ints[3] = res.Ints[3].Add(d);
    res.Ints[4] = res.Ints[4].Add(e);
    res.Ints[5] = res.Ints[5].Add(f);
    res.Ints[6] = res.Ints[6].Add(g);
    res.Ints[7] = res.Ints[7].Add(h);
    return res;
}

SHA::SHA(QByteArray input)
{
    state = Block256(
        IntValue(0x6a09e667),
        IntValue(0xbb67ae85),
        IntValue(0x3c6ef372),
        IntValue(0xa54ff53a),
        IntValue(0x510e527f),
        IntValue(0x9b05688c),
        IntValue(0x1f83d9ab),
        IntValue(0x5be0cd19));

    int bitlen = input.size() * 8;

    int len = input.size();
    int d = input.size() % 64;
    if (d < 56)
        input.resize(input.size() + 64 - d);
    else
        input.resize(input.size() + 64 - d + 64);

    input[len] = static_cast<char>(0x80);
    for (int i = len + 1; i < input.size(); ++i)
        input[i] = 0;

    for (int i = input.size() - 1; i > input.size() - 8; i--)
    {
        input[i] = static_cast<char>(bitlen);
        bitlen = bitlen >> 8;
    }

    IntValue ints[16];
    for (int i = 0; i < input.size() / 64; i++)
    {
        for (int j = 0; j < 16; j++)
        {
            int offset = i * 64 + j * 4;

             unsigned int val = ((input[offset] & 0xFF) << 24) |
                    ((input[offset + 1] & 0xFF) << 16) |
                    ((input[offset + 2] & 0xFF) << 8)  |
                    ( input[offset + 3] & 0xFF);

            ints[j] = IntValue(val);
        }

        state = SHARound(state, ints);
    }
}

QByteArray SHA::Result() const
{
    QByteArray result(32, 0);

    for (int i = 0; i < 8; i++)
    {
        uint v = state.Ints[i].Get();
        result[i * 4]     = static_cast<char>(v >> 24);
        result[i * 4 + 1] = static_cast<char>(v >> 16);
        result[i * 4 + 2] = static_cast<char>(v >> 8);
        result[i * 4 + 3] = static_cast<char>(v);
    }
    return result;
}

QList<BitValue *> SHA::OutBits() const
{
    return state.GetOutBits();
}
