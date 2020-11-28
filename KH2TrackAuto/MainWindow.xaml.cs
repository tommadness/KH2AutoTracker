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
    public partial class MainWindow : Window
    {
        private Int32 ADDRESS_OFFSET = 0;
        private static System.Timers.Timer aTimer;
        private List<ImportantCheck> importantChecks = new List<ImportantCheck>();
        private Ability highJump;
        private Ability quickRun;
        private Ability dodgeRoll;
        private Ability aerialDodge;
        private Ability glide;
        public MainWindow()
        {
            InitializeComponent();
            importantChecks.Add(highJump = new Ability(0x0032E0FE, ADDRESS_OFFSET));
            importantChecks.Add(quickRun = new Ability(0x0032E100, ADDRESS_OFFSET));
            importantChecks.Add(dodgeRoll = new Ability(0x0032E102, ADDRESS_OFFSET));
            importantChecks.Add(aerialDodge = new Ability(0x0032E104, ADDRESS_OFFSET));
            importantChecks.Add(glide = new Ability(0x0032E106, ADDRESS_OFFSET));
            findAddressOffset();
            SetBindings();
            SetTimer();
            OnTimedEvent(null, null);
        }

        private void findAddressOffset()
        {
            bool found = false;
            Int32 offset = 0x00000000;
            Int32 testAddr = 0x0010001C;
            string good = "280C";
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
            Binding HJbinding = new Binding("Level");
            HJbinding.Source = highJump;
            HighJumpLabel.SetBinding(Label.ContentProperty, HJbinding);
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
            importantChecks.ForEach(delegate (ImportantCheck importantCheck)
            {
                importantCheck.UpdateMemory();
            });
        }
        
        private string BytesToHex(byte[] bytes)
        {
            if (Enumerable.SequenceEqual(bytes,new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }))
            {
                return "Service not started. Waiting for PCSX2";
            }
            return BitConverter.ToString(bytes).Replace("-", "");
        }
    }
}

