using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;
using BellDetectWpf.ViewModels.MicStream;

namespace BellDetectWpf.Views.ViewPages
{
    public partial class MicStreamPage : Page
    {
        public MicStreamPage()
        {
            InitializeComponent();
        }

        private async void Load_Click(object sender, RoutedEventArgs e)
        {

            await C_MicStream.LoadDetectionSpec();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            await C_MicStream.SaveDetectionSpec();
        }

        private async void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            await C_MicStream.SaveAsDetectionSpec();
        }

        private async void StartStopBtn_Click(object sender, RoutedEventArgs e)
        {
            await MicStreamVM.StartStop();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\MainPage.xaml", UriKind.Relative);
        }
    }
}
