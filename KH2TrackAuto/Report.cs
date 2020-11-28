using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH2TrackAuto
{
    class Report : ImportantCheck
    {
        public string Content;

        public Report(int address, int offset) : base(address,offset)
        {

        }
    }
}
