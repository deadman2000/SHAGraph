#ifndef GRAPHWIDGET_H
#define GRAPHWIDGET_H

#include <QOpenGLWidget>
#include <QOpenGLFunctions>
#include <QTimer>

#include "camera.h"

class GraphWidget : public QOpenGLWidget, protected QOpenGLFunctions
{
    Q_OBJECT

public:
    GraphWidget(QWidget *parent = 0);

protected:
    void initializeGL() override;
    void paintGL() override;
    void resizeGL(int nWidth, int nHeight) override;

    void tick();

    void keyPressEvent(QKeyEvent *event) override;
    void mouseMoveEvent(QMouseEvent *event) override;
    void mousePressEvent(QMouseEvent *event) override;
    void mouseReleaseEvent(QMouseEvent *event) override;
    void wheelEvent(QWheelEvent *event) override;

private:
    QTimer timer;

    Camera camera;
};

#endif // GRAPHWIDGET_H
