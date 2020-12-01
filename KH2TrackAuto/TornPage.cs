using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH2TrackAuto
{
    class TornPage : ImportantCheck
    {
        public int Quantity;

        public TornPage(MemoryReader mem, int address, int offset) : base(mem, address, offset)
        {

        }
    }
}
