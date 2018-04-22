#ifndef BLOCK256_H
#define BLOCK256_H

#include "intvalue.h"

class Block256
{
public:
    IntValue Ints[8];

    Block256();

    Block256(const IntValue & v0,
             const IntValue & v1,
             const IntValue & v2,
             const IntValue & v3,
             const IntValue & v4,
             const IntValue & v5,
             const IntValue & v6,
             const IntValue & v7);
};

#endif // BLOCK256_H
