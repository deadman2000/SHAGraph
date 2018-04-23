#include "not.h"

NOT::NOT(BitValue * in1)
    : _input(in1)
{
}

BitValue **NOT::GetInputs()
{
    return &_input;
}

int NOT::GetInputsCount()
{
    return 1;
}

QString NOT::GetName()
{
    return "NOT";
}

bool NOT::Calc()
{
    return !_input->Get();
}
