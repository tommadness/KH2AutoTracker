using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace KH2TrackAuto
{
    class Report : ImportantCheck, INotifyPropertyChanged
    {
        public string Content;
        private int byteNum;
        public ReportsHandler reportsHandler;
        private bool obtained;

        public new bool Obtained { get => obtained; set 
            {
                obtained = value;
                OnPropertyChanged("Count");
            }
        }

        public Report(MemoryReader mem, int address, int offset, int byteNumber, ReportsHandler reportsHandler) : base(mem, address,offset)
        {
            byteNum = byteNumber;
            Bytes = 2;
            this.reportsHandler = reportsHandler;
        }
        public override byte[] UpdateMemory()
        {
            byte[] data = base.UpdateMemory();
            Obtained = new BitArray(data)[byteNum];
            return null;
        }

        public new event PropertyChangedEventHandler PropertyChanged;
        public new void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(reportsHandler.Reports, new PropertyChangedEventArgs(info));
            }
        }
    }
}
