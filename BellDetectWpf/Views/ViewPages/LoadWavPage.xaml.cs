using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;
using BellDetectWpf.ViewModels.Waveform;
using BellDetectWpf.Views.ViewPages;
using Microsoft.Win32;

namespace BellDetectWpf.Views
{
    public partial class LoadWavPage : Page
    {
        readonly LoadWavVM viewModel;

        public LoadWavPage()
        {
            InitializeComponent();
            viewModel = new();
            this.DataContext = viewModel;
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            viewModel.LoadWav();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new MainPage());
        }
    }
}
