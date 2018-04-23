#ifndef GRAPHWIDGET_H
#define GRAPHWIDGET_H

#include <QOpenGLWidget>
#include <QOpenGLFunctions>
#include <QTimer>
#include <QSet>

#include <forceatlas2.h>
#include "camera.h"
#include "bitvalue.h"

class GraphWidget : public QOpenGLWidget, protected QOpenGLFunctions
{
    Q_OBJECT

public:
    GraphWidget(QWidget *parent = 0);

    void setNodes(QList<BitValue*> nodes);
    void setMaxDepth(int depth);

protected:
    void initializeGL() override;
    void paintGL() override;
    void resizeGL(int nWidth, int nHeight) override;

    void tick();

    void keyPressEvent(QKeyEvent *event) override;
    void keyReleaseEvent(QKeyEvent *event) override;
    void mouseMoveEvent(QMouseEvent *event) override;
    void mousePressEvent(QMouseEvent *event) override;
    void mouseReleaseEvent(QMouseEvent *event) override;
    void wheelEvent(QWheelEvent *event) override;

private:
    QTimer timer;
    Camera camera;
    QSet<int> keys;
    QList<BitValue*> nodes;
    int max_depth;

    ForceAtlas2 force;
};

#endif // GRAPHWIDGET_H
