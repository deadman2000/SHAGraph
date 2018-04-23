#include "intvalue.h"

#include <QList>

#include "constantbit.h"
#include "xor.h"
#include "and.h"
#include "or.h"
#include "not.h"

IntValue::IntValue()
{
}

IntValue::IntValue(int value)
{
    for (int i = 0; i < 32; ++i)
        Bits[i] = new ConstantBit(((value >> i) & 1) == 1);
}

IntValue::IntValue(unsigned int value)
{
    for (int i = 0; i < 32; ++i)
        Bits[i] = new ConstantBit(((value >> i) & 1) == 1);
}

IntValue::IntValue(ByteValue & b0, ByteValue & b1, ByteValue & b2, ByteValue & b3)
{
    for (int i = 0; i < 8; ++i)
    {
        Bits[i] = b0.Bits[i];
        Bits[i + 8] = b1.Bits[i];
        Bits[i + 16] = b2.Bits[i];
        Bits[i + 24] = b3.Bits[i];
    }
}

unsigned int IntValue::Get() const
{
    unsigned int value = 0;
    for (int i = 0; i < 32; ++i)
    {
        if (Bits[i]->Get())
            value |= (1 << i);
    }
    return value;
}

IntValue IntValue::ShiftCirc(int bits)
{
    IntValue val;
    for (int i = 0; i < 32; ++i)
    {
        int s = i + bits;
        if (s > 31) s -= 32;
        val.Bits[i] = Bits[s];
    }
    return val;
}

IntValue IntValue::Shift(int bits)
{
    IntValue val;
    for (int i = 0; i < 32; ++i)
    {
        int s = i + bits;
        if (s >= 32)
            val.Bits[i] = new ConstantBit(false);
        else
            val.Bits[i] = Bits[s];
    }
    return val;
}

IntValue IntValue::And(const IntValue &other) const
{
    IntValue val;
    for (int i = 0; i < 32; i++)
        val.Bits[i] = new AND(Bits[i], other.Bits[i]);
    return val;
}

IntValue IntValue::Xor(const IntValue &other) const
{
    IntValue val;
    for (int i = 0; i < 32; ++i)
        val.Bits[i] = new XOR(Bits[i], other.Bits[i]);
    return val;
}

IntValue IntValue::Not() const
{
    IntValue val;
    for (int i = 0; i < 32; ++i)
        val.Bits[i] = new NOT(Bits[i]);
    return val;
}

IntValue IntValue::Add(const IntValue & other) const
{
    IntValue val;
    val.Bits[0] = new XOR(Bits[0], other.Bits[0]);
    BitValue * carry = new AND(Bits[0], other.Bits[0]);
    XOR * x0;
    for (int i = 1; i < 32; i++)
    {
        val.Bits[i] = new XOR(x0 = new XOR(Bits[i], other.Bits[i]), carry);
        if (i != 31) // Не считаем остаток для последнего бита
            carry = new OR(new AND(x0, carry), new AND(Bits[i], other.Bits[i]));
    }
    return val;
}

QList<BitValue *> IntValue::OutBits() const
{
    QList<BitValue *> bits;
    bits.reserve(32);
    for (int i=0; i<32; i++)
        bits.append(Bits[i]);
    return bits;
}
