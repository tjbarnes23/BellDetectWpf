using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;
using BellDetectWpf.ViewModels.FFT;

namespace BellDetectWpf.Views.ViewPages
{
    public partial class FFTPage : Page
    {
        public FFTPage()
        {
            InitializeComponent();
        }

        private async void RunFFT_Click(object sender, RoutedEventArgs e)
        {
            await C_FFT.RunFFT();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            await C_FFT.SaveFFT();
        }

        private async void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            await C_FFT.SaveAsFFT();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\MainPage.xaml", UriKind.Relative);
        }
    }
}
