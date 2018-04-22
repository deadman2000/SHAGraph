using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaCalc.Model
{
    class OutputInt : IntValue
    {
        public OutputInt(IntValue input)
        {
            for (int i = 0; i < 32; i++)
                Bits[i] = new OutputBit(input[i]);
        }
    }
}
