#include "forceatlas2.h"

#include <QDebug>
#include <QtMath>

void ForceAtlas2::work()
{
    while (!stopped) {
        if (started){
            doAlg();
        } else {
            QThread::msleep(100);
        }
    }
}

ForceAtlas2::ForceAtlas2()
{
    this->moveToThread(&forceThread);
    connect(&forceThread, &QThread::started, this, &ForceAtlas2::work);
    forceThread.start();
}

ForceAtlas2::~ForceAtlas2()
{
    stopped = true;
    forceThread.quit();
    forceThread.wait();
}

void ForceAtlas2::prepare()
{
    speed = 1;
    speedEfficiency = 1;

    foreach (BitValue* n, nodes) {
        if (n->depth > max_depth)
            n->mass = 1;
        else
            n->mass = 1 + n->GetInputsCount();
    }

    foreach (BitValue* n, nodes) {
        if (n->depth > max_depth) continue;
        for (int i=0; i<n->GetInputsCount(); i++) {
            BitValue* ib = n->GetInputs()[i];
            ib->mass++;
        }

        n->old_dx = 0;
        n->old_dy = 0;
        n->old_dz = 0;
        n->dx = 0;
        n->dy = 0;
        n->dz = 0;
    }

    if (distributedAttraction) {
        outboundAttCompensation = 0;
        foreach (BitValue* n, nodes) {
            outboundAttCompensation += n->mass;
        }
        outboundAttCompensation /= nodes.size();
    }

    Repulsion = RepulsionForce::buildRepulsion(isAdjustSizes, scalingRatio);
    GravityForce = strongGravityMode ? RepulsionForce::getStrongGravity(scalingRatio) : Repulsion;
    Attraction = AttractionForce::buildAttraction(isLinLogMode, distributedAttraction, isAdjustSizes, distributedAttraction ? outboundAttCompensation : 1);
}

void ForceAtlas2::doAlg()
{
    foreach (BitValue* n, nodes) {
        if (n->depth > max_depth) continue;
        n->old_dx = n->dx;
        n->old_dy = n->dy;
        n->old_dz = n->dz;
        n->dx = 0;
        n->dy = 0;
        n->dz = 0;
    }

    if (isBarnesHutOptimize) {
        if (rootRegion) // TODO Reuse region
            delete rootRegion;
        rootRegion = new Region(nodes);
        rootRegion->buildSubRegions();
    }

    /*QList<NodesThread*> threads;
    int taskCount = 8 * threadCount;  // The threadPool Executor Service will manage the fetching of tasks and threads.
    for (int t = taskCount; t > 0; t--) {
        int from = (int) qFloor(nodes.size() * (t - 1) / taskCount);
        int to = (int) qFloor(nodes.size() * t / taskCount);
        NodesThread * thr = new NodesThread(nodes, from, to);
        threads.append(thr);
    }*/

    // Repulsion
    if (isBarnesHutOptimize) {
        foreach (BitValue* n, nodes) {
            if (n->depth > max_depth) continue;
            rootRegion->applyForce(n, Repulsion, barnesHutTheta * barnesHutTheta);
        }
    } else {
        for (int n1Index = 0; n1Index < nodes.size(); n1Index++) {
            BitValue * n1 = nodes[n1Index];
            if (n1->depth > max_depth) continue;
            for (int n2Index = 0; n2Index < n1Index; n2Index++) {
                BitValue * n2 = nodes[n2Index];
                if (n2->depth > max_depth) continue;
                Repulsion->apply(n1, n2);
            }
        }
    }

    // Gravity
    if (gravity > 0) {
        foreach (BitValue* n, nodes) {
            if (n->depth > max_depth) continue;
            GravityForce->apply(n, gravity / scalingRatio);
        }
    }

    // Attraction
    foreach (BitValue* n, nodes) {
        if (n->depth >= max_depth) continue;
        for (int i=0; i<n->GetInputsCount(); ++i) {
            BitValue* s = n->GetInputs()[i];
            Attraction->apply(s, n, n->depth + 1);
        }
    }

    // Auto adjust speed
    double totalSwinging = 0;  // How much irregular movement
    double totalEffectiveTraction = 0;  // Hom much useful movement
    foreach (BitValue* n, nodes) {
        if (n->depth > max_depth) continue;
        //if (!n.isFixed()) {
        double swinging = DIST3D(n->old_dx - n->dx, n->old_dy - n->dy, n->old_dz - n->dz);
        totalSwinging += n->mass * swinging;   // If the node has a burst change of direction, then it's not converging.
        totalEffectiveTraction += n->mass * 0.5 * DIST3D(n->old_dx + n->dx, n->old_dy + n->dy, n->old_dz + n->dz);
    }

    // Optimize jitter tolerance
    // The 'right' jitter tolerance for this network. Bigger networks need more tolerance. Denser networks need less tolerance. Totally empiric.
    double estimatedOptimalJitterTolerance = 0.05 * qSqrt(nodes.size());
    double minJT = qSqrt(estimatedOptimalJitterTolerance);
    double maxJT = 10;
    double jt = jitterTolerance * qMax(minJT, qMin(maxJT, estimatedOptimalJitterTolerance * totalEffectiveTraction / qPow(nodes.size(), 2)));

    double minSpeedEfficiency = 0.05;

    // Protection against erratic behavior
    if (totalSwinging / totalEffectiveTraction > 2.0) {
        if (speedEfficiency > minSpeedEfficiency) {
            speedEfficiency *= 0.5;
        }
        jt = qMax(jt, jitterTolerance);
    }

    double targetSpeed = jt * speedEfficiency * totalEffectiveTraction / totalSwinging;

    // Speed efficiency is how the speed really corresponds to the swinging vs. convergence tradeoff
    // We adjust it slowly and carefully
    if (totalSwinging > jt * totalEffectiveTraction) {
        if (speedEfficiency > minSpeedEfficiency) {
            speedEfficiency *= 0.7;
        }
    } else if (speed < 1000) {
        speedEfficiency *= 1.3;
    }

    // But the speed shoudn't rise too much too quickly, since it would make the convergence drop dramatically.
    double maxRise = 0.5;   // Max rise: 50%
    speed = speed + qMin(targetSpeed - speed, maxRise * speed);


    if (isAdjustSizes) {
        foreach (BitValue* n, nodes) {
            if (n->depth > max_depth) continue;
            //if (!n.isFixed()) {

            // Adaptive auto-speed: the speed of each node is lowered
            // when the node swings.
            double swinging = n->mass * DIST3D(n->old_dx - n->dx, n->old_dy - n->dy, n->old_dz - n->dz);
            double factor = 0.1 * speed / (1.0 + qSqrt(speed * swinging));

            double df = DIST3D(n->dx, n->dy, n->dz);
            factor = qMin(factor * df, 10.) / df;

            double x = n->x + n->dx * factor;
            double y = n->y + n->dy * factor;
            double z = n->z + n->dz * factor;

            n->x = x;
            n->y = y;
            n->z = z;
        }
    } else {
        foreach (BitValue* n, nodes) {
            if (n->depth > max_depth) continue;
            //if (!n.isFixed()) {
            // Adaptive auto-speed: the speed of each node is lowered
            // when the node swings.
            double swinging = n->mass * DIST3D(n->old_dx - n->dx, n->old_dy - n->dy, n->old_dz - n->dz);
            double factor = speed / (1.0 + qSqrt(speed * swinging));

            double x = n->x + n->dx * factor;
            double y = n->y + n->dy * factor;
            double z = n->z + n->dz * factor;

            n->x = x;
            n->y = y;
			n->z = z;
        }
    }
}
