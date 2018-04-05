using System.Drawing;

namespace ShaCalc.Rendering
{
    class ConstantBitStyle : BlockStyle
    {
        public ConstantBitStyle(bool value)
        {
            Width = 2;
            Height = 2;
            BorderColor = Color.Black;
            Text = value ? "1" : "0";

            Output = new BlockGate(2, 1);
        }
    }
}
