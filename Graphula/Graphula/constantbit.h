#ifndef CONSTANTBIT_H
#define CONSTANTBIT_H

#include "bitvalue.h"

class ConstantBit : public BitValue
{
public:
    ConstantBit(bool value);

    // BitValue interface
public:
    virtual BitValue *GetInputs() override;
    virtual int GetInputsCount() override;
    virtual QString GetName() override;

protected:
    virtual bool Calc() override;
};

#endif // CONSTANTBIT_H
