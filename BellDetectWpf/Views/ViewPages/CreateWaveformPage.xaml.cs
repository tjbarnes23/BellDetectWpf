using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;
using BellDetectWpf.ViewModels.CreateWaveform;

namespace BellDetectWpf.Views.ViewPages
{
    public partial class CreateWaveformPage : Page
    {
        public CreateWaveformPage()
        {
            InitializeComponent();
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateWaveform_Click(object sender, RoutedEventArgs e)
        {
            CreateWaveformVM.CreateWaveform();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\MainPage.xaml", UriKind.Relative);
        }
    }
}
