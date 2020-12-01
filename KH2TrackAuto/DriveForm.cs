using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;

namespace KH2TrackAuto
{
    class DriveForm : ImportantCheck
    {
        public int Level;
        private int byteNum;
        private int levelAddr;

        public DriveForm(MemoryReader mem, int address, int offset, int byteNumber, int levelAddress) : base(mem, address, offset)
        {
            byteNum = byteNumber;
            levelAddr = levelAddress;
            Bytes = 2;
        }

        public override byte[] UpdateMemory()
        {
            byte[] data = base.UpdateMemory();
            Obtained = new BitArray(data)[byteNum];
            Level = memory.ReadMemory(levelAddr + ADDRESS_OFFSET, Bytes)[0];
            return null;
        }
    }
}
