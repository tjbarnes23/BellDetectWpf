using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;
using BellDetectWpf.ViewModels.WavFile;

namespace BellDetectWpf.Views.ViewPages
{
    /// <summary>
    /// Interaction logic for WavFilePage.xaml
    /// </summary>
    public partial class WavFilePage : Page
    {
        public WavFilePage()
        {
            InitializeComponent();
        }

        private void LoadWav_Click(object sender, RoutedEventArgs e)
        {
            L2LoadWav.LoadWav();
        }

        private void SaveWav_Click(object sender, RoutedEventArgs e)
        {
            L2SaveWav.SaveWav();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\MainPage.xaml", UriKind.Relative);
        }
    }
}
