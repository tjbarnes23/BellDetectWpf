﻿using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;

namespace BellDetectWpf.Views
{
    public partial class EllipticPage : Page
    {
        readonly EllipticVM viewModel;

        public EllipticPage()
        {
            InitializeComponent();
            viewModel = new();
            this.DataContext = viewModel;
        }

        private async void Run_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.RunElliptic();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.SaveElliptic();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new MainPage());
        }
    }
}
