using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Repository;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels
{
    public partial class ButterworthVM
    {
        public async Task RunButterworth()
        {
            ButterworthStatus = "Applying Butterworth filter...";
            await Task.Delay(25);


            ButterworthStatus = "Butterworth filter applied";
            await Task.Delay(25);
        }
    }
}
