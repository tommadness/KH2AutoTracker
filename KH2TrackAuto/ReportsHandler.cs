using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH2TrackAuto
{
    class ReportsHandler
    {
        private List<Report> reports;

        public ReportsHandler()
        {

        }

        public void CollectReports(List<Report> reports)
        {
            this.reports = reports;
        }

        public List<Report> Reports { get 
            {
                List<Report> obtained = new List<Report>();

                foreach (Report report in reports)
                {
                    if (report.Obtained)
                    {
                        obtained.Add(report);
                    }
                }
                return obtained;
            
            }
            set => reports = value; }
    }
}
