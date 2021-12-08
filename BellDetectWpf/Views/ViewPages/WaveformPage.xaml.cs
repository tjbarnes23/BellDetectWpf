using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;
using BellDetectWpf.ViewModels.Waveform;

namespace BellDetectWpf.Views.ViewPages
{
    public partial class WaveformPage : Page
    {
        public WaveformPage()
        {
            InitializeComponent();
        }

        private void GenerateWaveform_Click(object sender, RoutedEventArgs e)
        {
            C_Waveform.GenerateWaveform();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\MainPage.xaml", UriKind.Relative);
        }
    }
}
