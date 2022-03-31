using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Enums;
using BellDetectWpf.Repository;

namespace BellDetectWpf.ViewModels
{
    public partial class KeyPressVM
    {
        public static async Task ExecuteKeyPresses()
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

                Press(rand);
                await Task.Delay(1000);
            }
            while (Repo.KeyPressMode == KeyPressModeEnum.Running);
        }
    }
}
