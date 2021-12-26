using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.ViewModels
{
    public static class SharedVM
    {
        private static string statusMsg;
        private static string statusForeground;

        public static event EventHandler StatusMsgChanged;
        public static event EventHandler StatusForegroundChanged;

        public static string StatusMsg
        {
            get
            {
                return statusMsg;
            }
            set
            {
                if (statusMsg != value)
                {
                    statusMsg = value;
                    StatusMsgChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static string StatusForeground
        {
            get
            {
                return statusForeground;
            }
            set
            {
                if (statusForeground != value)
                {
                    statusForeground = value;
                    StatusForegroundChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }
    }
}
