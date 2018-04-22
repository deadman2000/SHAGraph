#ifndef OR_H
#define OR_H

#include "bitvalue.h"

class OR : public BitValue
{
private:
    BitValue * _inputs[2];

public:
    OR(BitValue * in1, BitValue * in2);

    // BitValue interface
public:
    virtual BitValue *GetInputs() override;
    virtual int GetInputsCount() override;
    virtual QString GetName() override;

protected:
    virtual bool Calc() override;
};

#endif // OR_H
