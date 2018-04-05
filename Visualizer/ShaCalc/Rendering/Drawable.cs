using ShaCalc.Model;
using System;
using System.Drawing;
using Tao.OpenGl;

namespace ShaCalc.Rendering
{
    abstract class Drawable
    {
        public int X = int.MinValue, Y = int.MinValue;

        public void Draw()
        {
            if (!IsPlaced()) return;

            Gl.glPushMatrix();
            Gl.glTranslated(X, Y, 0);
            var style = GetStyle();
            style.Draw();
            Gl.glPopMatrix();

            // Рисуем связи
            Gl.glBegin(Gl.GL_LINES);

            for (int i = 0; i < style.Input.Count; i++)
            {
                var gate = style.Input[i];
                var from = GetInput(i);
                var fromGate = from.GetStyle().Output;

                Visualizer.SetColor(from.GetOutColor());
                
                Gl.glVertex2i(from.X + fromGate.X, from.Y + fromGate.Y);
                Gl.glVertex2i(X + gate.X, Y + gate.Y);
            }

            Gl.glEnd();
        }

        private bool IsPlaced()
        {
            return X != int.MinValue && Y != int.MinValue;
        }

        protected abstract Color GetOutColor();

        public abstract BlockStyle GetStyle();

        public abstract Drawable GetInput(int n);
    }
}
