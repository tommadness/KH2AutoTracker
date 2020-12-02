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
        private int level = 0;
        public int Level
        {
            get { return level; }
            set
            {
                level = value;
                OnPropertyChanged("Level");
            }
        }
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
            byte[] levelData = memory.ReadMemory(levelAddr + ADDRESS_OFFSET, 1);
            if (Obtained == true)
            {
                Level = levelData[0];
            }
            if (levelData[0] > 1)
            {
                Level = levelData[0];
            }
            return null;
        }
    }
}
