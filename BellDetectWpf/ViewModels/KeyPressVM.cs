using System;
using System.Threading.Tasks;
using BellDetectWpf.ViewModels.KeyPress;
using BellDetectWpf.ViewModels.Shared;

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
            if (StartStopTxt == "Start key presses")
            {
                await C_Shared.Status("Generating key presses", "red", 10, false);
                StartStopTxt = "Stop key presses";
                await StartKeyPresses();
            }
            else
            {
                await C_Shared.Status(string.Empty, "black", 10, false);
                StartStopTxt = "Start key presses";
            }
        }

        public static async Task StartKeyPresses()
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

                C_KeyPress.Press(rand);
                await Task.Delay(1000);
            }
            while (StartStopTxt == "Stop key presses");
        }
    }
}
