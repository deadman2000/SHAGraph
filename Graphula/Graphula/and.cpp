#include "and.h"

AND::AND(BitValue * in1, BitValue * in2)
{
    _inputs[0] = in1;
    _inputs[1] = in2;
}


BitValue *AND::GetInputs()
{
    return _inputs[0];
}

int AND::GetInputsCount()
{
    return 2;
}

QString AND::GetName()
{
    return "AND";
}

bool AND::Calc()
{
    return _inputs[0]->Get() && _inputs[1]->Get();
}
