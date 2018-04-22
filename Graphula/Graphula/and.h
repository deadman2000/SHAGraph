#ifndef AND_H
#define AND_H

#include "bitvalue.h"

class AND : public BitValue
{
private:
    BitValue * _inputs[2];

public:
    AND(BitValue * in1, BitValue * in2);

    // BitValue interface
public:
    virtual BitValue *GetInputs() override;
    virtual int GetInputsCount() override;
    virtual QString GetName() override;

protected:
    virtual bool Calc() override;
};

#endif // AND_H
