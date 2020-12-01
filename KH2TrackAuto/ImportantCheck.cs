using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace KH2TrackAuto
{
    class ImportantCheck : INotifyPropertyChanged
    {
        public string Name;
        public int Address;
        public int Bytes = 1;
        public bool Obtained;
        public int ADDRESS_OFFSET;

        public MemoryReader memory;

        public ImportantCheck(MemoryReader mem, int address, int offset)
        {
            ADDRESS_OFFSET = offset;
            Address = address;
            memory = mem;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }

        public virtual byte[] UpdateMemory()
        {
            return memory.ReadMemory(Address + ADDRESS_OFFSET, Bytes);
        }
    }
}
