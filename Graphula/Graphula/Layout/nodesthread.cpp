#include "nodesthread.h"

NodesThread::NodesThread(QList<BitValue *> &nodes, int from, int to)
    : nodes(nodes), from(from), to(to)
{
}

void NodesThread::run() {
    // Repulsion
    for (int nIndex = from; nIndex < to; nIndex++) {
        BitValue* n = nodes[nIndex];
        //rootRegion->applyForce(n, Repulsion, barnesHutTheta);
    }

    // Gravity
    for (int nIndex = from; nIndex < to; nIndex++) {
        BitValue* n = nodes[nIndex];
        //GravityForce::apply(n, gravity / scaling);
    }
}
