using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BellDetectWpf
{
    public static class ViewModel
    {
        private static string startStopTxt;
        public static event EventHandler StartStopTxtChanged;
        
        public static MainWindow Mw { get; set; }

        public static string StartStopTxt
        {
            get
            {
                return startStopTxt;
            }

            set
            {
                if (startStopTxt != value)
                {
                    startStopTxt = value;

                    // The loading of a method to call into the StartStopTxtChanged event is done by the compiler,
                    // which sees we have a XAML element that we are binding to a property called StartStopTxt,
                    // and it checks if there is a corresponding StartStopTxtChanged event.
                    // If there is, it loads a built-in method to be called into the event,
                    // and two-way binding is therefore established.
                    StartStopTxtChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static void Initialize()
        {
            Mw = new MainWindow();
            StartStopTxt = "Start detecting";
            Mw.Activate();
        }

        public static async Task StartStop()
        {
            if (StartStopTxt == "Start detecting")
            {
                StartStopTxt = "Stop detecting";
                await StartDetecting();
            }
            else
            {
                StartStopTxt = "Start detecting";
            }
        }

        public static async Task StartDetecting()
        {
            var random = new Random();
            await Task.Delay(2000);

            do
            {
                byte rand = (byte)random.Next(0, 2);

                if (rand == 0)
                {
                    rand = 0x21; // f key
                }
                else
                {
                    rand = 0x24; // j key
                }

                KeyPress.Press(rand);
                await Task.Delay(1000);
            }
            while (StartStopTxt == "Stop detecting");
        }
    }
}
