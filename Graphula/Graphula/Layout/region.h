#ifndef REGION_H
#define REGION_H

#include "repulsionforce.h"

#include <QList>
#include <bitvalue.h>

class Region
{
public:
    Region(QList<BitValue*> nodes);
    ~Region();

    void updateMassAndGeometry();
    void buildSubRegions();
    void applyForce(BitValue* n, RepulsionForce* Force, double theta);

    double mass = 0;
    double massCenterX = 0;
    double massCenterY = 0;
    double massCenterZ = 0;
    double size = 0;
    QList<BitValue*> nodes;
    QList<Region*> subregions;
};

#endif // REGION_H
