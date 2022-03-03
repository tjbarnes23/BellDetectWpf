using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;
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

        private async void Load_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.LoadWav();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new MainPage());
        }
    }
}
