#ifndef REPULSIONFORCE_H
#define REPULSIONFORCE_H

#include <bitvalue.h>

class Region;

class RepulsionForce
{
public:
    static RepulsionForce * buildRepulsion(bool adjustBySize, double coefficient);
    static RepulsionForce * getStrongGravity(double coefficient);

    virtual void apply(BitValue* n1, BitValue* n2) = 0;           // Model for node-node repulsion

    virtual void apply(BitValue* n, Region* r) = 0;           // Model for Barnes Hut approximation

    virtual void apply(BitValue* n, double g) = 0;           // Model for gravitation (anti-repulsion)
};

#endif // REPULSIONFORCE_H
