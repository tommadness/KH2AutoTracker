using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH2TrackAuto
{
    class TornPage : ImportantCheck
    {
        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        public TornPage(MemoryReader mem, int address, int offset) : base(mem, address, offset)
        {

        }

        public override byte[] UpdateMemory()
        {
            byte[] data = base.UpdateMemory();
            Quantity = data[0];
            return null;
        }
    }
}
