#include "region.h"
#include <float.h>
#include <QtMath>
#include <QDebug>

Region::Region(QList<BitValue*> nodes)
    : nodes(nodes)
{
    updateMassAndGeometry();
    //qDebug() << "Created region" << nodes.size();
}

Region::~Region()
{
    qDeleteAll(subregions);
}

void Region::updateMassAndGeometry()
{
    if (nodes.size() <= 1) return;

    // Compute Mass
    mass = 0;
    double massSumX = 0;
    double massSumY = 0;
    double massSumZ = 0;
    foreach (BitValue * n, nodes) {
        mass += n->mass;
        massSumX += n->x * n->mass;
        massSumY += n->y * n->mass;
        massSumZ += n->z * n->mass;
    }
    massCenterX = massSumX / mass;
    massCenterY = massSumY / mass;
    massCenterZ = massSumZ / mass;

    // Compute size
    size = 0;
    foreach (BitValue * n, nodes) {
        double distance = DIST3D(n->x - massCenterX, n->y - massCenterY, n->z - massCenterZ);
        size = qMax(size, 2 * distance);
    }
    size = size*size;

    /*foreach (Region* subregion, subregions) {
        subregion->updateMassAndGeometry();
    }*/
}

void Region::buildSubRegions()
{
    if (nodes.size() <= 1) return;

    QList<BitValue*> upDownNodes[2];
    foreach (BitValue * n, nodes) {
        QList<BitValue*> & nodesColumn = (n->z < massCenterZ) ? (upDownNodes[0]) : (upDownNodes[1]);
        nodesColumn.append(n);
    }

    for (int z=0; z<2; z++)
    {
        QList<BitValue*> & zSubNodes = upDownNodes[z];

        QList<BitValue*> leftRightNodes[2];
        foreach (BitValue * n, zSubNodes) {
            QList<BitValue*> & nodesColumn = (n->x < massCenterX) ? (leftRightNodes[0]) : (leftRightNodes[1]);
            nodesColumn.append(n);
        }

        for (int x=0; x<2; x++){
            QList<BitValue*> & xSubNodes = upDownNodes[x];

            QList<BitValue*> topBottomNodes[2];
            foreach (BitValue * n, xSubNodes) {
                QList<BitValue*> & nodesLine = (n->y < massCenterY) ? (topBottomNodes[0]) : (topBottomNodes[1]);
                nodesLine.append(n);
            }

            for (int y=0; y<2; y++){
                QList<BitValue*> & ySubNodes = topBottomNodes[y];

                if (ySubNodes.size() > 0) {
                    if (ySubNodes.size() < nodes.size()) {
                        Region* subregion = new Region(ySubNodes);
                        subregions.append(subregion);
                    } else {
                        foreach (BitValue * n, ySubNodes) {
                            QList<BitValue*> oneNodeList;
                            oneNodeList.append(n);
                            Region* subregion = new Region(oneNodeList);
                            subregions.append(subregion);
                        }
                    }
                }
            }
        }
    }

    foreach (Region* subregion, subregions) {
        subregion->buildSubRegions();
    }
}

void Region::applyForce(BitValue *n, RepulsionForce *Force, double thetaSq)
{
    if (nodes.size() < 2) {
        BitValue* regionNode = nodes[0];
        Force->apply(n, regionNode);
    } else {
        double distance = DIST3DSQ(n->x - massCenterX, n->y - massCenterY, n->z - massCenterZ); // TODO Square dist
        if (distance * thetaSq > size) {
            Force->apply(n, this);
        } else {
            foreach (Region* subregion, subregions) {
                subregion->applyForce(n, Force, thetaSq);
            }
        }
    }
}
