﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace BellDetectWpf.Views
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void LoadWav_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new LoadWavPage());
        }

        private void FFT_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new FFTPage());
        }

        private void DFT_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new DFTPage());
        }

        private void Elliptic_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new EllipticPage());
        }

        private void FIR_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new FIRPage());
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
