using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;
using BellDetectWpf.ViewModels.WavFile;

namespace BellDetectWpf.Views.ViewPages
{
    public partial class WavFilePage : Page
    {
        public WavFilePage()
        {
            InitializeComponent();
        }

        private void LoadWav_Click(object sender, RoutedEventArgs e)
        {
            C_WavFile.LoadWav();
        }

        private void SaveWav_Click(object sender, RoutedEventArgs e)
        {
            C_WavFile.SaveWav();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\MainPage.xaml", UriKind.Relative);
        }
    }
}
