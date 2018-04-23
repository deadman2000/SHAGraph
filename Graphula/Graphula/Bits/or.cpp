#include "or.h"

OR::OR(BitValue * in1, BitValue * in2)
{
    _inputs[0] = in1;
    _inputs[1] = in2;
}

BitValue **OR::GetInputs()
{
    return _inputs;
}

int OR::GetInputsCount()
{
    return 2;
}

QString OR::GetName()
{
    return "OR";
}

bool OR::Calc()
{
    return _inputs[0]->Get() || _inputs[1]->Get();
}
