using System.Drawing;

namespace ShaCalc.Rendering
{
    class OutputBitStyle : BlockStyle
    {
        public OutputBitStyle(bool value)
        {
            Width = 2;
            Height = 2;
            BorderColor = Color.Black;
            Text = value ? "1" : "0";

            Input.Add(new BlockGate(0, 1));
        }
    }
}
