using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;

namespace BellDetectWpf.Views
{
    public partial class DetectionPage : Page
    {
        readonly DetectionVM viewModel;

        public DetectionPage()
        {
            InitializeComponent();
            viewModel = new();
            this.DataContext = viewModel;
        }

        private async void Run_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.RunDetection();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.SaveDetection();
        }

        private void FIR_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new FIRPage());
        }


        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new MainPage());
        }

        private void MicStream_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new MicStreamPage());
        }
    }
}
