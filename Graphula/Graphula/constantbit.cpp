#include "constantbit.h"

ConstantBit::ConstantBit(bool value)
{
    _isCalc = true;
    _value = value;
}


BitValue *ConstantBit::GetInputs()
{
    return nullptr;
}

int ConstantBit::GetInputsCount()
{
    return 0;
}

QString ConstantBit::GetName()
{
    if (_value) return "1";
    return "0";
}

bool ConstantBit::Calc()
{
    return _value;
}
