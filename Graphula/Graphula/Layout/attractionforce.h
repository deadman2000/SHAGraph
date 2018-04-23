#ifndef ATTRACTIONFORCE_H
#define ATTRACTIONFORCE_H

#include "bitvalue.h"

class AttractionForce
{
public:
    virtual void apply(BitValue * n1, BitValue * n2, double e) = 0; // Model for node-node attraction (e is for edge weight if needed)

    static AttractionForce * buildAttraction(bool logAttraction, bool distributedAttraction, bool adjustBySize, double coefficient);
};

#endif // ATTRACTIONFORCE_H
