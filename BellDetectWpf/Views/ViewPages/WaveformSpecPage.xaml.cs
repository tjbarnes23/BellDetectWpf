using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BellDetectWpf.ViewModels;
using BellDetectWpf.ViewModels.WaveformSpec;
using Microsoft.Win32;

namespace BellDetectWpf.Views.ViewPages
{
    /// <summary>
    /// Interaction logic for SpecifyWaveform.xaml
    /// </summary>
    public partial class WaveformSpecPage : Page
    {
        public WaveformSpecPage()
        {
            InitializeComponent();
        }
        private async void Load_Click(object sender, RoutedEventArgs e)
        {
            
            await C_WaveformSpec.LoadWaveformSpec();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            await C_WaveformSpec.SaveWaveformSpec();
        }

        private async void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            await C_WaveformSpec.SaveAsWaveformSpec();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\MainPage.xaml", UriKind.Relative);
        }
    }
}
