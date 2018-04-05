namespace Visualizer.Model
{
    class NOT32 : IntValue
    {
        public NOT32(IntValue x)
        {
            for (int i = 0; i < 32; i++)
            {
                Bits[i] = new NOT(x[i]);
            }
        }
    }
}