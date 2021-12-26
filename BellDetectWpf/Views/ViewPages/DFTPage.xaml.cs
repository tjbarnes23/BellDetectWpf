using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;
using BellDetectWpf.ViewModels.DFT;

namespace BellDetectWpf.Views.ViewPages
{
    public partial class DFTPage : Page
    {
        public DFTPage()
        {
            InitializeComponent();
        }

        private async void RunDFT_Click(object sender, RoutedEventArgs e)
        {
            await C_DFT.RunDFT();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            await C_DFT.SaveDFT();
        }

        private async void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            await C_DFT.SaveAsDFT();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\MainPage.xaml", UriKind.Relative);
        }
    }
}
