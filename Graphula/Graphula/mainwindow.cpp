#include "mainwindow.h"
#include "ui_mainwindow.h"

#include <QQueue>
#include <QRandomGenerator>
#include <QDebug>

#include "sha.h"

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    ui->graphView->setFocus();

    int max_depth = 5;

    /*IntValue a(32);
    IntValue b(54);
    auto c = a.Add(b);
    QList<BitValue*> bits = c.OutBits();*/

    QByteArray data(63, 0);
    SHA sha(data);
    QList<BitValue*> bits = sha.OutBits();

    QSet<BitValue*> allBits;
    QQueue<BitValue*> queue;
    foreach (BitValue* b, bits)
    {
        b->depth = 0;
        queue.enqueue(b);
        allBits.insert(b);
    }

    while (!queue.isEmpty())
    {
        BitValue* b = queue.dequeue();
        int d = b->depth + 1;
        if (d > max_depth)
            continue;

        for (int i=0; i<b->GetInputsCount(); ++i)
        {
            BitValue* ib = b->GetInputs()[i];
            if (ib->depth == 0)
                ib->depth = d;
            if (!allBits.contains(ib))
            {
                queue.append(ib);
                allBits.insert(ib);
                bits.append(ib);
            }
        }
    }

    QRandomGenerator rnd;
    foreach (BitValue* b, bits)
    {
        b->x = rnd.bounded(-500, 500);
        b->y = rnd.bounded(-500, 500);
        //b->z = rnd.bounded(-500, 500);
        b->z = b->depth * 100;
    }

    ui->graphView->setMaxDepth(max_depth);
    ui->graphView->setNodes(bits);
}

MainWindow::~MainWindow()
{
    delete ui;
}
