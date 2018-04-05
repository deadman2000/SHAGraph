using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaCalc.Model
{
    class IdGenerator
    {
        private int _nextID = 0;

        public int GetID()
        {
            return _nextID++;
        }
    }
}
