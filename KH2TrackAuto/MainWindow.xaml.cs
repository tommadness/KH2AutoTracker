using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.ComponentModel;

namespace KH2TrackAuto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        const int HIGH_JUMP_ADDRESS = 0x0032E0FE;
        const int ABILITY_BYTES = 2;
        const int DEFAULT_BYTES = 1;
        private int ADDRESS_OFFSET = 0;
        private static System.Timers.Timer aTimer;

        private string hjContent;
        public string HJContent {
            get { return hjContent; }
            set { hjContent = value;
                OnPropertyChanged("HJContent");}
        }
        public MainWindow()
        {
            InitializeComponent();
            findAddressOffset();
            SetBindings();
            SetTimer();
            OnTimedEvent(null, null);
        }

        private void findAddressOffset()
        {
            bool found = false;
            int offset = 0x00000000;
            int testAddr = 0x0010001C;
            string good = "0C28";
            while (!found)
            {
                string tester = BytesToHex(new MemoryReader(testAddr + offset, 2).ReadMemory());
                if (tester == "Service not started. Waiting for PCSX2")
                {
                    break;
                }
                else if (tester == good)
                {
                    found = true;
                }
                else
                {
                    offset = offset + 0x10000000;
                }
            }
            ADDRESS_OFFSET = offset;
        }

        private void SetBindings()
        {
            Binding HJbinding = new Binding("HJContent");
            HJbinding.Source = this;
            HighJump.SetBinding(Label.ContentProperty, HJbinding);
        }
        private void SetTimer()
        {
            aTimer = new System.Timers.Timer(66);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (ADDRESS_OFFSET == 0)
            {
                findAddressOffset();
            }
            HJContent = BytesToHex(new MemoryReader(HIGH_JUMP_ADDRESS+ADDRESS_OFFSET, ABILITY_BYTES).ReadMemory());
        }
        
        private string BytesToHex(byte[] bytes)
        {
            if (Enumerable.SequenceEqual(bytes,new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }))
            {
                return "Service not started. Waiting for PCSX2";
            }
            return BitConverter.ToString(bytes).Replace("-", "");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}

