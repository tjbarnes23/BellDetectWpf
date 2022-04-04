using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;
using Microsoft.Win32;

namespace BellDetectWpf.Views
{
    public partial class LoadWavSpecPage : Page
    {
        readonly LoadWavSpecVM viewModel;

        public LoadWavSpecPage()
        {
            InitializeComponent();
            viewModel = new();
            this.DataContext = viewModel;
        }

        private void LoadWavSpec_Click(object sender, RoutedEventArgs e)
        {
            LoadWavSpecVM.LoadWavSpec();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new WavSpecPage());
        }
    }
}
