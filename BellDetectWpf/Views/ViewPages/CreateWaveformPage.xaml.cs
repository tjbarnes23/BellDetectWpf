using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;
using BellDetectWpf.ViewModels.Waveform;
using Microsoft.Win32;

namespace BellDetectWpf.Views.ViewPages
{
    public partial class CreateWaveformPage : Page
    {
        public CreateWaveformPage()
        {
            InitializeComponent();
        }

        private void LoadDefaultWaveform_Click(object sender, RoutedEventArgs e)
        {
            C_Waveform.DefaultWaveform();
        }

        private void ClearWaveform_Click(object sender, RoutedEventArgs e)
        {
            C_Waveform.ClearWaveform();
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog
            {
                Filter = string.Empty,
                InitialDirectory = @"C:\ProgramData\BellDetect"
            };

            if (saveDlg.ShowDialog() == true)
            {
                WaveformVM.FilePathName = saveDlg.FileName;
            }
        }

        private void CreateWaveform_Click(object sender, RoutedEventArgs e)
        {
            WaveformVM.CreateWaveform();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\MainPage.xaml", UriKind.Relative);
        }

        
    }
}
