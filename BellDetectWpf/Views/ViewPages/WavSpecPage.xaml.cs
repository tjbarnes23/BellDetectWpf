using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;
using Microsoft.Win32;

namespace BellDetectWpf.Views
{
    public partial class WavSpecPage : Page
    {
        readonly WavSpecVM viewModel;

        public WavSpecPage()
        {
            InitializeComponent();
            viewModel = new();
            this.DataContext = viewModel;
        }

        private void ManageWavSpec_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new ManageWavSpecPage());
        }

        private void CreateWav_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new CreateWavPage());
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new MainPage());
        }
    }
}
