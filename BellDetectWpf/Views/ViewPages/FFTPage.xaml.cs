using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;

namespace BellDetectWpf.Views.ViewPages
{
    public partial class FFTPage : Page
    {
        public FFTPage()
        {
            InitializeComponent();
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {

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
