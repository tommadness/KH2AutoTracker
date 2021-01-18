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
using System.Runtime.InteropServices;
namespace KH2TrackAuto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MemoryReader memory;

        private Int32 ADDRESS_OFFSET;
        private static System.Timers.Timer aTimer;
        private List<ImportantCheck> importantChecks;
        private List<Report> reports;
        private ReportsHandler reportsHandler;
        private Ability highJump;
        private Ability quickRun;
        private Ability dodgeRoll;
        private Ability aerialDodge;
        private Ability glide;

        private DriveForm valor;
        private DriveForm wisdom;
        private DriveForm master;
        private DriveForm limit;
        private DriveForm final;

        private Magic fire;
        private Magic blizzard;
        private Magic thunder;
        private Magic magnet;
        private Magic reflect;
        private Magic cure;

        private Report rep1;
        private Report rep2;
        private Report rep3;
        private Report rep4;
        private Report rep5;
        private Report rep6;
        private Report rep7;
        private Report rep8;
        private Report rep9;
        private Report rep10;
        private Report rep11;
        private Report rep12;
        private Report rep13;

        private Summon chickenLittle;
        private Summon stitch;
        private Summon genie;
        private Summon peterPan;

        private ImportantCheck promiseCharm;
        private ImportantCheck peace;
        private ImportantCheck nonexist;
        private ImportantCheck connection;

        private TornPage pages;

        public MainWindow()
        {
            InitializeComponent();
            do
            {
                memory = new MemoryReader();
            } while (!memory.Hooked);
            this.FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./resources/#KH2 ALL MENU");
            findAddressOffset();

            importantChecks = new List<ImportantCheck>();
            importantChecks.Add(highJump = new Ability(memory, 0x0032E0FE, ADDRESS_OFFSET, 93));
            importantChecks.Add(quickRun = new Ability(memory, 0x0032E100, ADDRESS_OFFSET, 97));
            importantChecks.Add(dodgeRoll = new Ability(memory, 0x0032E102, ADDRESS_OFFSET, 563));
            importantChecks.Add(aerialDodge = new Ability(memory, 0x0032E104, ADDRESS_OFFSET, 101));
            importantChecks.Add(glide = new Ability(memory, 0x0032E106, ADDRESS_OFFSET, 105));

            importantChecks.Add(valor = new DriveForm(memory, 0x0032F1F0, ADDRESS_OFFSET, 1, 0x0032EE26));
            importantChecks.Add(wisdom = new DriveForm(memory, 0x0032F1F0, ADDRESS_OFFSET, 2, 0x0032EE5E));
            importantChecks.Add(master = new DriveForm(memory, 0x0032F1F0, ADDRESS_OFFSET, 6, 0x0032EE26));
            importantChecks.Add(limit = new DriveForm(memory, 0x0032F1FA, ADDRESS_OFFSET, 3, 0x0032EE26));
            importantChecks.Add(final = new DriveForm(memory, 0x0032F1F0, ADDRESS_OFFSET, 4, 0x0032EE26));

            importantChecks.Add(fire = new Magic(memory, 0x0032F0C4, ADDRESS_OFFSET));
            importantChecks.Add(blizzard = new Magic(memory, 0x0032F0C5, ADDRESS_OFFSET));
            importantChecks.Add(thunder = new Magic(memory, 0x0032F0C6, ADDRESS_OFFSET));
            importantChecks.Add(cure = new Magic(memory, 0x0032F0C7, ADDRESS_OFFSET));
            importantChecks.Add(magnet = new Magic(memory, 0x0032F0FF, ADDRESS_OFFSET));
            importantChecks.Add(reflect = new Magic(memory, 0x0032F100, ADDRESS_OFFSET));

            reportsHandler = new ReportsHandler();

            //importantChecks.Add(rep1 = new Report(memory, 0x0032F1F4, ADDRESS_OFFSET, 6));
            //importantChecks.Add(rep2 = new Report(memory, 0x0032F1F4, ADDRESS_OFFSET, 7));
            //importantChecks.Add(rep3 = new Report(memory, 0x0032F1F5, ADDRESS_OFFSET, 0));
            //importantChecks.Add(rep4 = new Report(memory, 0x0032F1F5, ADDRESS_OFFSET, 1));
            importantChecks.Add(rep5 = new Report(memory, 0x0032F1F5, ADDRESS_OFFSET, 2, reportsHandler));
            //importantChecks.Add(rep6 = new Report(memory, 0x0032F1F5, ADDRESS_OFFSET, 3));
            //importantChecks.Add(rep7 = new Report(memory, 0x0032F1F5, ADDRESS_OFFSET, 4));
            //importantChecks.Add(rep8 = new Report(memory, 0x0032F1F5, ADDRESS_OFFSET, 5));
            //importantChecks.Add(rep9 = new Report(memory, 0x0032F1F5, ADDRESS_OFFSET, 6));
            //importantChecks.Add(rep10 = new Report(memory, 0x0032F1F5, ADDRESS_OFFSET, 7));
            //importantChecks.Add(rep11 = new Report(memory, 0x0032F1F6, ADDRESS_OFFSET, 1));
            //importantChecks.Add(rep12 = new Report(memory, 0x0032F1F6, ADDRESS_OFFSET, 2));
            //importantChecks.Add(rep13 = new Report(memory, 0x0032F1F6, ADDRESS_OFFSET, 3));

            reports = new List<Report>();

            /*reports.Add(rep1);
            reports.Add(rep2);
            reports.Add(rep3);
            reports.Add(rep4);*/
            reports.Add(rep5);
            /*reports.Add(rep6);
            reports.Add(rep7);
            reports.Add(rep8);
            reports.Add(rep9);
            reports.Add(rep10);
            reports.Add(rep11);
            reports.Add(rep12);
            reports.Add(rep13);*/

            reportsHandler.CollectReports(reports);


            foreach (Report report in reports)
            {
                report.reportsHandler = reportsHandler;
            }

            importantChecks.Add(chickenLittle = new Summon(memory, 0x0032F1F0, ADDRESS_OFFSET, 3));
            importantChecks.Add(stitch = new Summon(memory, 0x0032F1F0, ADDRESS_OFFSET, 0));
            importantChecks.Add(genie = new Summon(memory, 0x0032F1F4, ADDRESS_OFFSET, 4));
            importantChecks.Add(peterPan = new Summon(memory, 0x0032F1F4, ADDRESS_OFFSET, 5));

            importantChecks.Add(promiseCharm = new Proof(memory, 0x0032F1C4, ADDRESS_OFFSET));
            importantChecks.Add(peace = new Proof(memory, 0x0032F1E4, ADDRESS_OFFSET));
            importantChecks.Add(nonexist = new Proof(memory, 0x0032F1E3, ADDRESS_OFFSET));
            importantChecks.Add(connection = new Proof(memory, 0x0032F1E2, ADDRESS_OFFSET));

            importantChecks.Add(pages = new TornPage(memory, 0x0032F0C8, ADDRESS_OFFSET));

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
                string tester = BytesToHex(memory.ReadMemory(testAddr + offset, 2));
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

            Binding reportBinding = new Binding("Count");
            reportBinding.Source = reportsHandler.Reports;
            ReportsLabel.SetBinding(ContentControl.ContentProperty, reportBinding);



            highJump.BindLabel(HighJumpLabel, "Level");
            highJump.BindImage(HighJumpImage, "Obtained");
            quickRun.BindLabel(QuickRunLabel, "Level");
            quickRun.BindImage(QuickRunImage, "Obtained");
            aerialDodge.BindLabel(AerialDodgeLabel, "Level");
            aerialDodge.BindImage(AerialDodgeImage, "Obtained");
            dodgeRoll.BindLabel(DodgeRollLabel, "Level");
            dodgeRoll.BindImage(DodgeRollImage, "Obtained");
            glide.BindLabel(GlideLabel, "Level");
            glide.BindImage(GlideImage, "Obtained");

            valor.BindLabel(ValorLabel, "Level");
            valor.BindImage(ValorImage, "Obtained");
            wisdom.BindLabel(WisdomLabel, "Level");
            wisdom.BindImage(WisdomImage, "Obtained");
            master.BindLabel(MasterLabel, "Level");
            master.BindImage(MasterImage, "Obtained");
            limit.BindLabel(LimitLabel, "Level");
            limit.BindImage(LimitImage, "Obtained");
            final.BindLabel(FinalLabel, "Level");
            final.BindImage(FinalImage, "Obtained");

            fire.BindLabel(FireLabel, "Level");
            fire.BindImage(FireImage, "Obtained");
            blizzard.BindLabel(BlizzardLabel, "Level");
            blizzard.BindImage(BlizzardImage, "Obtained");
            thunder.BindLabel(ThunderLabel, "Level");
            thunder.BindImage(ThunderImage, "Obtained");
            cure.BindLabel(CureLabel, "Level");
            cure.BindImage(CureImage, "Obtained");
            magnet.BindLabel(MagnetLabel, "Level");
            magnet.BindImage(MagnetImage, "Obtained");
            reflect.BindLabel(ReflectLabel, "Level");
            reflect.BindImage(ReflectImage, "Obtained");

            pages.BindLabel(TornPagesLabel, "Quantity", false);

            nonexist.BindImage(ProofNonExistenceImage, "Obtained");
            peace.BindImage(ProofPeaceImage, "Obtained");
            connection.BindImage(ProofConnectionImage, "Obtained");
            promiseCharm.BindImage(PromiseCharmImage, "Obtained");

            stitch.BindImage(StitchImage, "Obtained");
            chickenLittle.BindImage(ChickenLittleImage, "Obtained");
            genie.BindImage(GenieImage, "Obtained");
            peterPan.BindImage(PeterPanImage, "Obtained");
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
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }
    }
}

