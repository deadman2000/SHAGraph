#ifndef XOR_H
#define XOR_H

#include "bitvalue.h"

class XOR : public BitValue
{
private:
    BitValue * _inputs[2];

public:
    XOR(BitValue * in1, BitValue * in2);

    // BitValue interface
public:
    virtual BitValue *GetInputs() override;
    virtual int GetInputsCount() override;
    virtual QString GetName() override;

protected:
    virtual bool Calc() override;
};

#endif // XOR_H
