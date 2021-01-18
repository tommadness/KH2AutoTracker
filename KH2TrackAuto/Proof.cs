using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;

namespace KH2TrackAuto
{
    class Proof : ImportantCheck
    {
        public Proof(MemoryReader mem, int address, int offset) : base(mem, address,offset)
        {

        }
        public override byte[] UpdateMemory()
        {
            byte[] data = base.UpdateMemory();
            if (data[0] > 0)
            {
                this.Obtained = true;
            }
            return null;
        }
    }
}
