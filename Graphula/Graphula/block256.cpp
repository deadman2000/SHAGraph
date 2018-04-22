#include "block256.h"

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
