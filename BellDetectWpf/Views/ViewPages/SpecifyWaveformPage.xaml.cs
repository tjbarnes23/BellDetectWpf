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
using Microsoft.Win32;

namespace BellDetectWpf.Views.ViewPages
{
    /// <summary>
    /// Interaction logic for SpecifyWaveform.xaml
    /// </summary>
    public partial class SpecifyWaveformPage : Page
    {
        public SpecifyWaveformPage()
        {
            InitializeComponent();
            // WaveformSpecDataGrid.ItemsSource = SpecifyWaveformVM.WaveformSpec;
        }
        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            SpecifyWaveformVM.WaveformSpec.Sort();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            SpecifyWaveformVM.Clear();
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            
            SpecifyWaveformVM.LoadWaveformSpec();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SpecifyWaveformVM.FilePathName))
            {
                SaveAs_Click(sender, e);
            }
            else
            {
                SpecifyWaveformVM.SaveWaveformSpec();
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog
            {
                Filter = string.Empty,
                InitialDirectory = @"C:\ProgramData\BellDetect"
            };

            if (saveDlg.ShowDialog() == true)
            {
                SpecifyWaveformVM.FilePathName = saveDlg.FileName;
                SpecifyWaveformVM.SaveWaveformSpec();
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWinVM.Mw.MainFrame.Source = new Uri(@"..\..\Views\ViewPages\MainPage.xaml", UriKind.Relative);
        }

        
    }
}
