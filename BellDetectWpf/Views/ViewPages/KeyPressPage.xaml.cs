using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;

namespace BellDetectWpf.Views.ViewPages
{
    public partial class KeyPressPage : Page
    {
        public KeyPressPage()
        {
            InitializeComponent();
        }

        private async void StartStopBtn_Click(object sender, RoutedEventArgs e)
        {
            await KeyPressVM.StartStop();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\MainPage.xaml", UriKind.Relative);
        }
    }
}
