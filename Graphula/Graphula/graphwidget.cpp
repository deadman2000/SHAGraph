#include "graphwidget.h"

//#include <GL/glu.h>
#include <QDebug>

GraphWidget::GraphWidget(QWidget *parent)
    : QOpenGLWidget(parent)
{
    setMouseTracking(true);

    connect(&timer, &QTimer::timeout, this, &GraphWidget::tick);
    timer.start(25); // 40 FPS

    camera.SetMode(FREE);
    camera.SetPosition(glm::vec3(1, 1, 1));
    camera.SetLookAt(glm::vec3(0, 0, 0));
    camera.SetClipping(.1, 1000);
    camera.SetFOV(45);
}

void GraphWidget::initializeGL()
{
    initializeOpenGLFunctions();

    glEnable(GL_DEPTH_TEST);
    glEnable(GL_CULL_FACE);
}

void GraphWidget::paintGL()
{
    glClearColor(1, 1, 1, 1);
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
    glMatrixMode(GL_PROJECTION);
    glEnable(GL_DEPTH_TEST);
    glLoadIdentity();

    camera.Update();

    glBegin(GL_LINES);
        glColor3d(1, 0, 0);  // X - Red
        glVertex3d(0, 0, 0);
        glVertex3d(1, 0, 0);

        glColor3d(0, 1, 0);  // Y - Green
        glVertex3d(0, 0, 0);
        glVertex3d(0, 1, 0);

        glColor3d(0, 0, 1);  // Z - Blue
        glVertex3d(0, 0, 0);
        glVertex3d(0, 0, 1);
    glEnd();
}

void GraphWidget::resizeGL(int nWidth, int nHeight)
{
    glViewport(0, 0, nWidth, nHeight);
    camera.SetViewport(0, 0, nWidth, nHeight);
}

void GraphWidget::keyPressEvent(QKeyEvent *event)
{
    switch (event->key())
    {
    case Qt::Key_W:
        camera.Move(FORWARD);
        break;
    case Qt::Key_S:
        camera.Move(BACK);
        break;
    case Qt::Key_A:
        camera.Move(LEFT);
        break;
    case Qt::Key_D:
        camera.Move(RIGHT);
        break;
    case Qt::Key_R:
        camera.Move(UP);
        break;
    case Qt::Key_F:
        camera.Move(DOWN);
        break;
    }
}

void GraphWidget::mouseMoveEvent(QMouseEvent *event)
{
    camera.Move2D(event->pos().x(), event->pos().y());
}

void GraphWidget::mousePressEvent(QMouseEvent *event)
{
    if (event->button() == Qt::LeftButton)
    {
        camera.move_camera = true;
    }
}

void GraphWidget::mouseReleaseEvent(QMouseEvent *event)
{
    if (event->button() == Qt::LeftButton)
    {
        camera.move_camera = false;
    }
}

void GraphWidget::wheelEvent(QWheelEvent *event)
{
    if (event->delta() > 0)
        camera.camera_position_delta += camera.camera_up * .05f;
    else
        camera.camera_position_delta -= camera.camera_up * .05f;
}

void GraphWidget::tick()
{
    repaint();
}
