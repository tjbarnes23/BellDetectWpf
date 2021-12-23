using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;
using BellDetectWpf.ViewModels.LoadWav;
using Microsoft.Win32;

namespace BellDetectWpf.Views.ViewPages
{
    public partial class LoadWavPage : Page
    {
        public LoadWavPage()
        {
            InitializeComponent();
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog
            {
                Filter = string.Empty,
                InitialDirectory = @"C:\ProgramData\BellDetect"
            };

            if (openDlg.ShowDialog() == true)
            {
                WavFileVM.FilePathName = openDlg.FileName;
            }
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadWav_Click(object sender, RoutedEventArgs e)
        {
            WavFileVM.LoadWav();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\MainPage.xaml", UriKind.Relative);
        }
    }
}
