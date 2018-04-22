#ifndef NOT_H
#define NOT_H

#include "bitvalue.h"

class NOT : public BitValue
{
private:
    BitValue * _input;

public:
    NOT(BitValue * in1);

    // BitValue interface
public:
    virtual BitValue *GetInputs() override;
    virtual int GetInputsCount() override;
    virtual QString GetName() override;

protected:
    virtual bool Calc() override;
};

#endif // NOT_H
