#ifndef BITVALUE_H
#define BITVALUE_H

#include <QString>

#define NODE_SIZE (10.0)

#define DIST3D(DX, DY, DZ) (qSqrt((DX)*(DX) + (DY)*(DY) + (DZ)*(DZ)))
#define DIST3DSQ(DX, DY, DZ) ((DX)*(DX) + (DY)*(DY) + (DZ)*(DZ))
#define DIST2D(DX, DY)     (qSqrt((DX)*(DX) + (DY)*(DY)))


class BitValue
{
public:
    BitValue();
    virtual ~BitValue();

    int ID;

    bool Get();

    virtual BitValue** GetInputs() = 0;
    virtual int GetInputsCount() = 0;

    virtual QString GetName() = 0;

    int depth;
    float x, y, z;
    float dx, dy, dz, old_dx, old_dy, old_dz;
    float mass;
    int outCount;

protected:
    virtual bool Calc() = 0;

protected:
    bool _isCalc;
    bool _value;
};

#endif // BITVALUE_H
