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

        private void SaveWaveform_Click(object sender, RoutedEventArgs e)
        {
            L2SaveWaveform.SaveWaveform();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\MainPage.xaml", UriKind.Relative);
        }

        
    }
}
