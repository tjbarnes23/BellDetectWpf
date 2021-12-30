using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;

namespace BellDetectWpf.Views.ViewPages
{
    public partial class MicStreamPage : Page
    {
        public MicStreamPage()
        {
            InitializeComponent();
        }

        private void StartStopBtn_Click(object sender, RoutedEventArgs e)
        {
            MicStreamVM.StartStop();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\MainPage.xaml", UriKind.Relative);
        }
    }
}
