using System;
using System.Collections.Generic;
using System.Drawing;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace ShaCalc.Rendering
{
    // Правила рисования блока
    class BlockStyle
    {
        public int Width, Height;
        public Color BackgroundColor = Color.FromArgb(240, 240, 240);
        public Color BorderColor;
        public string Text;

        public List<BlockGate> Input = new List<BlockGate>();
        public BlockGate Output;

        public void Draw()
        {
            SetColor(BackgroundColor);
            Gl.glRecti(0, 0, Width, Height);

            SetColor(BorderColor);
            DrawRect(Width, Height);
            if (!String.IsNullOrEmpty(Text))
                DrawText(1 - 0.25 * Text.Length, 1, Text);

            Gl.glPointSize(6f);
            Gl.glBegin(Gl.GL_POINTS);

            foreach (var g in Input)
                Gl.glVertex2i(g.X, g.Y);

            if (Output != null)
                Gl.glVertex2i(Output.X, Output.Y);

            Gl.glEnd();
        }

        protected void DrawText(double posx, double posy, string c)
        {
            Gl.glRasterPos2d(posx, posy);
            for (int i = 0; i < c.Length; i++)
                Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_TIMES_ROMAN_24, c[i]);
        }

        protected void DrawRect(int width, int height)
        {
            Gl.glLineWidth(2f);
            Gl.glBegin(Gl.GL_LINE_LOOP);
            Gl.glVertex2i(0, 0);
            Gl.glVertex2i(0, height);
            Gl.glVertex2i(width, height);
            Gl.glVertex2i(width, 0);
            Gl.glEnd();
        }

        protected void SetColor(Color color)
        {
            Visualizer.SetColor(color);
        }
    }
}
