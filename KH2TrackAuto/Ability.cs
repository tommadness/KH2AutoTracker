using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH2TrackAuto
{
    class Ability : ImportantCheck
    {
        const int ADDRESS_START = 0x0032E074;
        const int ADDRESS_END = 0x0032E112;

        private int level;
        public int Level { get { return level; }
            set
            {
                level = value;
                OnPropertyChanged("Level");
            }
        }

        private int levelOffset;

        public Ability(int address, int offset) : base(address, offset)
        {
            Bytes = 2;
        }

        public override byte[] UpdateMemory()
        {
            byte[] data = base.UpdateMemory();
            Console.WriteLine(BitConverter.ToString(data));
            Level = BitConverter.ToUInt16(data,0);
            return null;
        }
    }
}
