﻿using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;
using Microsoft.Win32;

namespace BellDetectWpf.Views
{
    public partial class CreateWavPage : Page
    {
        readonly CreateWavVM viewModel;

        public CreateWavPage()
        {
            InitializeComponent();
            viewModel = new();
            this.DataContext = viewModel;
        }

        private async void Create_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.CreateWav();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.SaveWav();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new MainPage());
        }
    }
}
