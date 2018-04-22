#include "xor.h"

XOR::XOR(BitValue * in1, BitValue * in2)
{
    _inputs[0] = in1;
    _inputs[1] = in2;
}


BitValue *XOR::GetInputs()
{
    return _inputs[0];
}

int XOR::GetInputsCount()
{
    return 2;
}

QString XOR::GetName()
{
    return "XOR";
}

bool XOR::Calc()
{
    return _inputs[0]->Get() ^ _inputs[1]->Get();
}
