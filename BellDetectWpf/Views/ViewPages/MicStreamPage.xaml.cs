using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;

namespace BellDetectWpf.Views
{
    public partial class MicStreamPage : Page
    {
        readonly MicStreamVM viewModel;

        public MicStreamPage()
        {
            InitializeComponent();
            viewModel = new();
            this.DataContext = viewModel;
        }

        private async void StartStop_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.MicStreamStartStop();
        }

        private void Detection_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new DetectionPage());
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new MainPage());
        }
    }
}
