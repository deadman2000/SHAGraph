#include "bitvalue.h"

static int NEXT_ID = -1;

BitValue::BitValue()
    : ID(++NEXT_ID)
    , _isCalc(false)
{
}

BitValue::~BitValue()
{
}

bool BitValue::Get()
{
    if (_isCalc) return _value;
    _value = Calc();
    _isCalc = true;
    return _value;
}
