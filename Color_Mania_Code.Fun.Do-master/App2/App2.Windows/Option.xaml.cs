using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Option : Page
    {
        public Option()
        {
            this.InitializeComponent();
        }

        private void reset_scores(object sender, PointerRoutedEventArgs e)
        {
            ApplicationDataContainer scores = ApplicationData.Current.LocalSettings;
            scores.Values["0"] = "0";
            scores.Values["1"] = "0";
            scores.Values["2"] = "0";
            return;
        }

        private void Menu_back(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
