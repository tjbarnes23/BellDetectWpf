using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;
using BellDetectWpf.ViewModels.Waveform;
using Microsoft.Win32;

namespace BellDetectWpf.Views.ViewPages
{
    public partial class WaveformPage : Page
    {
        public WaveformPage()
        {
            InitializeComponent();
        }

        private async void Create_Click(object sender, RoutedEventArgs e)
        {
            await C_Waveform.CreateWaveform();
        }

        private async void Load_Click(object sender, RoutedEventArgs e)
        {
            await C_Waveform.LoadWaveform();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            await C_Waveform.SaveWaveform();
        }

        private async void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            await C_Waveform.SaveAsWaveform();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\MainPage.xaml", UriKind.Relative);
        }
    }
}
