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
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\WaveformPage.xaml", UriKind.Relative);
        }

        private void WavFile_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\WavFilePage.xaml", UriKind.Relative);
        }

        private void FFT_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\FFTPage.xaml", UriKind.Relative);
        }

        private void KeyPress_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\KeyPressPage.xaml", UriKind.Relative);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            L2Exit.Exit();
        }
    }
}
