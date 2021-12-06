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
using BellDetectWpf.ViewModels;
using BellDetectWpf.ViewModels.FFT;

namespace BellDetectWpf.Views.ViewPages
{
    /// <summary>
    /// Interaction logic for FFTPage.xaml
    /// </summary>
    public partial class FFTPage : Page
    {
        public FFTPage()
        {
            InitializeComponent();
        }

        private void FFT_Click(object sender, RoutedEventArgs e)
        {
            FFTVM.RunFFT();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\MainPage.xaml", UriKind.Relative);
        }
    }
}
