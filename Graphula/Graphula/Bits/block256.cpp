#include "block256.h"

#include <QList>

Block256::Block256()
{
}

Block256::Block256(const IntValue &v0, const IntValue &v1, const IntValue &v2, const IntValue &v3, const IntValue &v4, const IntValue &v5, const IntValue &v6, const IntValue &v7)
{
    Ints[0] = v0;
    Ints[1] = v1;
    Ints[2] = v2;
    Ints[3] = v3;
    Ints[4] = v4;
    Ints[5] = v5;
    Ints[6] = v6;
    Ints[7] = v7;
}

QList<BitValue*> Block256::GetOutBits() const
{
    QList<BitValue*> bits;
    bits.reserve(8 * 32);
    for (int i = 0; i < 8; ++i)
    {
        for (int n = 0; n < 32; n++)
        {
            bits.append(Ints[i].Bits[n]);
        }
    }
    return bits;
}
