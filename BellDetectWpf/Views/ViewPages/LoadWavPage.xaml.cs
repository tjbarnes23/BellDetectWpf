using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;
using BellDetectWpf.ViewModels.LoadWav;

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
