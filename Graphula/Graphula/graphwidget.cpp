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
    camera.SetPosition(glm::vec3(500, 500, 500));
    camera.SetLookAt(glm::vec3(0, 0, 0));
    camera.SetClipping(.1, 100000);
    camera.SetFOV(45);
}

void GraphWidget::setNodes(QList<BitValue *> nodes)
{
    this->nodes = nodes;
    force.nodes = nodes;
    force.prepare();
}

void GraphWidget::setMaxDepth(int depth)
{
    max_depth = depth;
    force.max_depth = depth;
}

void GraphWidget::initializeGL()
{
    initializeOpenGLFunctions();

    //glEnable(GL_DEPTH_TEST);
    glEnable(GL_CULL_FACE);
    glEnable(GL_BLEND);
    glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
}

void GraphWidget::paintGL()
{
    glClearColor(1, 1, 1, 1);
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
    glMatrixMode(GL_PROJECTION);
    //glEnable(GL_DEPTH_TEST);
    //glLoadIdentity();

    camera.Update();

    glBegin(GL_LINES);
        glColor3d(1, 0, 0);  // X - Red
        glVertex3d(0, 0, 0);
        glVertex3d(500, 0, 0);

        glColor3d(0, 1, 0);  // Y - Green
        glVertex3d(0, 0, 0);
        glVertex3d(0, 500, 0);

        glColor3d(0, 0, 1);  // Z - Blue
        glVertex3d(0, 0, 0);
        glVertex3d(0, 0, 500);
    glEnd();


	glPointSize(6.0);
    glLineWidth(2.f);
    foreach (BitValue* b, nodes)
    {
        if (b->depth >= max_depth) continue;

        if (b->depth == 0)
            glColor3d(1, 0, 0);
        else
            glColor3d(0.75, 0.75, 0.75);

		glBegin(GL_POINTS);
		glVertex3d(b->x, b->y, b->z);
		glEnd();

		if (b->depth == 0)
            glColor4d(1, 0, 0, 0.5);
		else
            glColor4d(0.75, 0.75, 0.75, 0.5);

		glBegin(GL_LINES);
        BitValue** ins = b->GetInputs();
        for (int i=0; i<b->GetInputsCount(); ++i)
        {
            BitValue* ib = ins[i];
			if (ib->depth >= max_depth) continue;

            glVertex3d(b->x, b->y, b->z);
            glVertex3d(ib->x, ib->y, ib->z);
        }
		glEnd();
    }
}

void GraphWidget::resizeGL(int nWidth, int nHeight)
{
    glViewport(0, 0, nWidth, nHeight);
    camera.SetViewport(0, 0, nWidth, nHeight);
}

void GraphWidget::tick()
{
    if (keys.contains(Qt::Key_W))
        camera.Move(FORWARD);
    if (keys.contains(Qt::Key_S))
        camera.Move(BACK);
    if (keys.contains(Qt::Key_A))
        camera.Move(LEFT);
    if (keys.contains(Qt::Key_D))
        camera.Move(RIGHT);
    if (keys.contains(Qt::Key_R))
        camera.Move(UP);
    if (keys.contains(Qt::Key_F))
        camera.Move(DOWN);

    repaint();
}

void GraphWidget::keyPressEvent(QKeyEvent *event)
{
    switch (event->key())
    {
    case Qt::Key_W:
    case Qt::Key_S:
    case Qt::Key_A:
    case Qt::Key_D:
    case Qt::Key_R:
    case Qt::Key_F:
        keys.insert(event->key());
        break;
    case Qt::Key_Space:
        force.started = !force.started;
        break;
    default:
        return;
    }
    event->accept();
}

void GraphWidget::keyReleaseEvent(QKeyEvent *event)
{
    switch (event->key())
    {
    case Qt::Key_W:
    case Qt::Key_S:
    case Qt::Key_A:
    case Qt::Key_D:
    case Qt::Key_R:
    case Qt::Key_F:
        keys.remove(event->key());
        break;
    default:
        return;
    }
    event->accept();
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
