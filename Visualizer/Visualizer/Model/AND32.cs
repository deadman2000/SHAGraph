using System;
namespace Visualizer.Model
{
    class AND32 : IntValue
    {
        public AND32(IntValue in1, IntValue in2)
        {
            for (int i = 0; i < 32; i++)
            {
                Bits[i] = new AND(in1[i], in2[i]);
            }
        }
    }
}
