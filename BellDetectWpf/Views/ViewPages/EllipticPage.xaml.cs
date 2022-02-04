using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;
using BellDetectWpf.ViewModels.Elliptic;
using BellDetectWpf.ViewModels.FFT;

namespace BellDetectWpf.Views.ViewPages
{
    public partial class EllipticPage : Page
    {
        public EllipticPage()
        {
            InitializeComponent();
        }

        private async void RunElliptic_Click(object sender, RoutedEventArgs e)
        {
            await C_Elliptic.RunElliptic();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            await C_Elliptic.SaveElliptic();
        }

        private async void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            await C_Elliptic.SaveAsElliptic();
        }

        private void WaveformPage_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\WaveformPage.xaml", UriKind.Relative);
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\MainPage.xaml", UriKind.Relative);
        }
    }
}
