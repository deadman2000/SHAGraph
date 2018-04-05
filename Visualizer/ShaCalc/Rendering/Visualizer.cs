using ShaCalc.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace ShaCalc.Rendering
{
    class Visualizer
    {
        private int _window;

        private double _cameraX = 0.0;
        private double _cameraY = 0.0;
        private double _cameraZ = 20.0;

        private double _pressCameraX, _pressCameraY;
        private int _pressX, _pressY;

        public readonly List<Drawable> Drawable = new List<Drawable>();

        public readonly List<OutputBit> Output = new List<OutputBit>();

        public Visualizer()
        {
            Glut.glutInit();
            Glut.glutInitWindowSize(500, 500);
            _window = Glut.glutCreateWindow("Visualizer");
            Glut.glutDisplayFunc(OnDisplay);
            Glut.glutReshapeFunc(OnReshape);
            Glut.glutMouseFunc(OnMouse);
            Glut.glutMotionFunc(OnMotion);
            Glut.glutMouseWheelFunc(OnMouseWheel);
        }

        public void AddOut(BitValue bit)
        {
            Drawable.Add(bit);

            OutputBit o = new OutputBit(bit);
            Output.Add(o);
            Drawable.Add(o);
        }

        public void Organize()
        {
            int y = 0;
            foreach (var d in Output)
            {
                Move(d, 0, y);
                y += d.GetStyle().Height + 2;
            }
        }

        private void Move(Drawable d, int x, int y)
        {
            d.X = x;
            d.Y = y;

            var style = d.GetStyle();

            for (int i = 0; i < style.Input.Count; i++)
            {
                var gate = style.Input[i];
                var from = d.GetInput(i);
                var fromGate = from.GetStyle().Output;
                
                int tx = d.X + gate.X - fromGate.X - 1;
                int ty = d.Y + gate.Y - fromGate.Y;
                Move(from, tx, ty);
            }
        }

        public void Start()
        {
            Glut.glutMainLoop();
        }

        private void Invalidate()
        {
            Glut.glutPostWindowRedisplay(_window);
        }

        private void OnDisplay()
        {
            Gl.glClearColor(1, 1, 1, 0);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glLoadIdentity();
            Glu.gluLookAt(
                _cameraX, _cameraY, -_cameraZ,
                _cameraX, _cameraY, 0,
                0, -1, 0);

            foreach (Drawable d in Drawable) d.Draw();

            Glut.glutSwapBuffers();
        }

        private void OnReshape(int w, int h)
        {
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glViewport(0, 0, w, h);
            //Glu.gluOrtho2D(0, 20, 0, 20);
            Glu.gluPerspective(40, (double)w / h, 1, 100);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
        }

        private void OnMotion(int x, int y)
        {
            //Console.WriteLine("motion {0}:{1}", x, y);
            const double moveD = 0.05;
            _cameraX = _pressCameraX + (_pressX - x) * moveD;
            _cameraY = _pressCameraY + (_pressY - y) * moveD;
            Invalidate();
        }

        private void OnMouse(int button, int state, int x, int y)
        {
            // Console.WriteLine("mouse Button: {0} State: {1} Pos: {2}:{3}", button, state, x, y);
            if (button == 0 && state == 0)
            {
                _pressX = x;
                _pressY = y;
                _pressCameraX = _cameraX;
                _pressCameraY = _cameraY;
            }
        }

        void OnMouseWheel(int wheel, int direction, int x, int y)
        {
            const double MIN_Z = 1;
            const double ZOOM_FACTOR = 1.5;

            if (direction > 0)
                _cameraZ = Math.Max(MIN_Z, _cameraZ / ZOOM_FACTOR);
            else
                _cameraZ = _cameraZ * ZOOM_FACTOR;

            Invalidate();
        }

        static Color _currentColor;
        public static void SetColor(Color color)
        {
            if (_currentColor == color) return;
            _currentColor = color;
            Gl.glColor4d(color.R / 255.0, color.G / 255.0, color.B / 255.0, color.A / 255.0);
        }
    }
}
