#ifndef BYTEVALUE_H
#define BYTEVALUE_H

#include "bitvalue.h"

class ByteValue
{
public:
    BitValue * Bits[8];

    ByteValue(char value);

    ByteValue(int value);

    char Get();
};

#endif // BYTEVALUE_H
