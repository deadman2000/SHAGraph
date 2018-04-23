#ifndef NODESTHREAD_H
#define NODESTHREAD_H

#include <QThread>
#include "bitvalue.h"

class NodesThread : public QThread
{
    Q_OBJECT

    void run() override;
public:
    NodesThread(QList<BitValue*> & nodes, int from, int to);

    QList<BitValue*> & nodes;
    int from, to;
};

#endif // NODESTHREAD_H
