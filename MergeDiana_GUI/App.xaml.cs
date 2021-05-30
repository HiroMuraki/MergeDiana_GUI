﻿using System;
using System.Windows;

namespace MergeDiana_GUI {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public static ResourceDictionary ResDict = new ResourceDictionary() {
            Source = new Uri("Resources/Images.xaml", UriKind.Relative)
        };

        private void Application_Startup(object sender, StartupEventArgs e) {
            MainWindow window = new MainWindow();
            window.Show();
        }
    }
}
