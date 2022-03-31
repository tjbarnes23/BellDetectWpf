using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;

namespace BellDetectWpf.Views
{
    public partial class ButterworthPage : Page
    {
        readonly ButterworthVM viewModel;

        public ButterworthPage()
        {
            InitializeComponent();
            viewModel = new();
            this.DataContext = viewModel;
        }

        private async void Run_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.RunButterworth();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.SaveButterworth();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new MainPage());
        }
    }
}
