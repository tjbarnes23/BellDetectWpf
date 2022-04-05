using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;
using Microsoft.Win32;

namespace BellDetectWpf.Views
{
    public partial class ManageWavSpecPage : Page
    {
        readonly ManageWavSpecVM viewModel;

        public ManageWavSpecPage()
        {
            InitializeComponent();
            viewModel = new();
            this.DataContext = viewModel;
        }

        private async void Load_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.LoadWavSpec();
            WaveSpecGrid.ItemsSource = null;
            WaveSpecGrid.ItemsSource = ManageWavSpecVM.WavSpecs;
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.SaveWavSpec();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new WavSpecPage());
        }
    }
}
