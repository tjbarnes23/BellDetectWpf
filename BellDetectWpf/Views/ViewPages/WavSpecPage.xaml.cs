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

        private void LoadWavSpec_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new LoadWavSpecPage());
        }

        private void CreateWavSpec_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new CreateWavSpecPage());
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
