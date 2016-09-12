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
    public sealed partial class High_Score : Page
    {
        string easy;
        string pro;
        string jedi;
        public High_Score()
        {
            
        this.InitializeComponent();

            if (scores.Values["0"] != null)
            {
                easy = scores.Values["0"].ToString();
            }
            else
            {
                easy = "0";
            }
            if (scores.Values["1"] != null)
            {
                pro = scores.Values["1"].ToString();
            }
            else
            {
                pro = "1";
            }
            if (scores.Values["2"] != null)
            {
                jedi = scores.Values["2"].ToString();
            }
            else
            {
                jedi = "0";
            }
        HighScores_Box.Text = "Easy - " + easy + "\n\nPro - " + pro + "\n\nJedi - " + jedi;
        }
        ApplicationDataContainer scores = ApplicationData.Current.LocalSettings;
        private void back_hs_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
