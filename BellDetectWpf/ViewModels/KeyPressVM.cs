using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.ViewModels.KeyPress;

namespace BellDetectWpf.ViewModels
{
    public static class KeyPressVM
    {
        private static string startStopTxt;
        public static event EventHandler StartStopTxtChanged;

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
                    StartStopTxtChanged?.Invoke(null, EventArgs.Empty);
                }
            }
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

                L2KeyPress.Press(rand);
                await Task.Delay(1000);
            }
            while (StartStopTxt == "Stop detecting");
        }
    }
}
