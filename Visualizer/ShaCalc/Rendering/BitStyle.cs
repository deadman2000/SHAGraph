using System.Drawing;

namespace ShaCalc.Rendering
{
    class BitStyle : BlockStyle
    {
        public BitStyle(string label, int inCount)
        {
            Width = 2;
            Height = 3;
            BorderColor = Color.Black;
            Text = label;

            for (int i = 0; i < inCount; i++)
                Input.Add(new BlockGate(0, i + 1));

            Output = new BlockGate(2, 1);
        }
    }
}
