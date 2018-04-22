#include "bytevalue.h"

#include "constantbit.h"

ByteValue::ByteValue(char value)
{
    for (int i = 0; i < 8; ++i)
        Bits[i] = new ConstantBit(((value >> i) & 1) == 1);
}

ByteValue::ByteValue(int value)
{
    for (int i = 0; i < 8; ++i)
        Bits[i] = new ConstantBit(((value >> i) & 1) == 1);
}

char ByteValue::Get()
{
    char value = 0;
    for (int i = 0; i < 8; ++i)
        if (Bits[i]->Get())
            value |= (1 << i);
    return value;
}
