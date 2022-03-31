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
        public async Task RunKeyPresses()
        {
            if (Repo.KeyPressMode == KeyPressModeEnum.Stopped)
            {
                KeyPressStatus = "Generating key presses";
                StartStopTxt = "Stop key presses";
                await Task.Delay(25);

                Repo.KeyPressMode = KeyPressModeEnum.Running;
                await ExecuteKeyPresses();
            }
            else
            {
                KeyPressStatus = "Key presses stopped";
                StartStopTxt = "Start key presses";
                await Task.Delay(25);

                Repo.KeyPressMode = KeyPressModeEnum.Stopped;
            }
        }
    }
}
