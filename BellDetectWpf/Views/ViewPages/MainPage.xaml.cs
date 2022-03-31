using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;

namespace BellDetectWpf.Views
{
    public partial class MainPage : Page
    {
        readonly MainVM viewModel;

        public MainPage()
        {
            InitializeComponent();
            viewModel = new();
            this.DataContext = viewModel;
        }

        private void LoadWav_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new LoadWavPage());
        }

        private void CreateWav_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new CreateWavPage());
        }

        private void FFT_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new FFTPage());
        }

        private void DFT_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new DFTPage());
        }

        private void Butterworth_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new ButterworthPage());
        }

        private void Elliptic_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new EllipticPage());
        }

        private void FIR_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new FIRPage());
        }

        private void KeyPress_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new KeyPressPage());
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
