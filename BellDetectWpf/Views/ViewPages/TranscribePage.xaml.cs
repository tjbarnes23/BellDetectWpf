using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;
using BellDetectWpf.ViewModels.Transcribe;

namespace BellDetectWpf.Views.ViewPages
{
    public partial class TranscribePage : Page
    {
        public TranscribePage()
        {
            InitializeComponent();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            await C_Transcribe.SaveTranscription();
        }

        private async void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            await C_Transcribe.SaveAsTranscription();
        }

        private void Detect_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\MicStreamPage.xaml", UriKind.Relative);
        }

        private async void StartStop_Click(object sender, RoutedEventArgs e)
        {
            await TranscribeVM.StartStop();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\MainPage.xaml", UriKind.Relative);
        }
    }
}
