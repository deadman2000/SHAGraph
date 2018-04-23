#ifndef FORCEATLAS2_H
#define FORCEATLAS2_H

#include <QObject>
#include <QList>
#include <QThread>

#include "bitvalue.h"
#include "region.h"
#include "attractionforce.h"
#include "repulsionforce.h"

class ForceAtlas2 : public QObject
{
    //Q_OBJECT
public:
    ForceAtlas2();
    ~ForceAtlas2();

    void prepare();

    void doAlg();

    bool started = false;

    QList<BitValue*> nodes;
    int max_depth;

    Region * rootRegion = nullptr;

    double outboundAttCompensation;
    double speedEfficiency = 1;
    double speed = 1;

    RepulsionForce * Repulsion;
    RepulsionForce * GravityForce;
    AttractionForce * Attraction;

    bool distributedAttraction = false;  // Ослабление хабов
    bool isLinLogMode = false;
    bool isAdjustSizes = false;          // Запрет перекрытия
    double edgeWeightInfluence = 1.0;    // Влияние весов ребер

    double jitterTolerance = 1.0;     // Устойчивость
    bool isBarnesHutOptimize = false;  // Приближенное отталкивание
    double barnesHutTheta = 1.2;

    double scalingRatio = 2.0;        // Разреженность
    bool strongGravityMode = false;   // Усиление гравитации
    double gravity = 1.0;             // Гравитация

public slots:
    void work();

private:
    QThread forceThread;
    bool stopped = false;
};

#endif // FORCEATLAS2_H
