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
        private static System.Timers.Timer aTimer;

        private int hjContent;
        public int HJContent {
            get { return hjContent; }
            set { hjContent = value;
                OnPropertyChanged("HJContent");}
        }
        public MainWindow()
        {
            InitializeComponent();
            SetBindings();
            SetTimer();
        }
        private void SetBindings()
        {
            Binding HJbinding = new Binding("HJContent");
            HJbinding.Source = this;
            HighJump.SetBinding(Label.ContentProperty, HJbinding);
        }
        private void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(500);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            HJContent = new MemoryReader(0x2032E0FE, 2).ReadMemory()[0];
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

