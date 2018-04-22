#ifndef BITVALUE_H
#define BITVALUE_H

#include <QString>

class BitValue
{
public:
    BitValue();
    virtual ~BitValue();

    int ID;

    bool Get();

    virtual BitValue* GetInputs() = 0;
    virtual int GetInputsCount() = 0;

    virtual QString GetName() = 0;

protected:
    virtual bool Calc() = 0;

protected:
    bool _isCalc;
    bool _value;
};

#endif // BITVALUE_H
