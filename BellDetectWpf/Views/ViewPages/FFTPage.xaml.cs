using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;

namespace BellDetectWpf.Views
{
    public partial class FFTPage : Page
    {
        readonly FFTVM viewModel;

        public FFTPage()
        {
            InitializeComponent();
            viewModel = new();
            this.DataContext = viewModel;
        }

        private async void Run_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.RunFFT();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.SaveFFT();
        }

        private async void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.SaveAsFFT();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new MainPage());
        }
    }
}
