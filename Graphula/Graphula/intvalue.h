#ifndef INTVALUE_H
#define INTVALUE_H

#include "bitvalue.h"
#include "bytevalue.h"

class IntValue
{
public:
    BitValue * Bits[32];

    IntValue();

    IntValue(int value);

    IntValue(unsigned int value);

    IntValue(ByteValue & b0, ByteValue & b1, ByteValue & b2, ByteValue & b3);

    unsigned int Get() const;

    IntValue ShiftCirc(int bits);
    IntValue Shift(int bits);

    IntValue And(const IntValue & val) const;
    IntValue Xor(const IntValue & val) const;
    IntValue Not() const;

    IntValue Add(const IntValue & val) const;
};

#endif // INTVALUE_H
