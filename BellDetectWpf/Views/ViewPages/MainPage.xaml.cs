using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;
using BellDetectWpf.ViewModels.MainWin;

namespace BellDetectWpf.Views.ViewPages
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void MicStream_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\MicStreamPage.xaml", UriKind.Relative);
        }

        private void Waveforms_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\CreateWaveformPage.xaml", UriKind.Relative);
        }

        private void WavFile_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\LoadWavPage.xaml", UriKind.Relative);
        }

        private void FFT_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\FFTPage.xaml", UriKind.Relative);
        }

        private void DFT_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\DFTPage.xaml", UriKind.Relative);
        }

        private void KeyPress_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\KeyPressPage.xaml", UriKind.Relative);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            C_MainWin.Exit();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
