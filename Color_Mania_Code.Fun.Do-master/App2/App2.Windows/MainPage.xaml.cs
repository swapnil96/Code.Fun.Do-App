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
    public sealed partial class MainPage : Page
    {
        public static String High_Score="0";

        public MainPage()
        {
            InitializeComponent();
        }

        private void Play_Event(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(level));
        }

        private void How_To_Play_navig(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Instructions2));
        }

        private void Exit_Call(object sender, PointerRoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void HS_Link(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(High_Score));
        }

        private void navig_about(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(About_Us));
        }

        private void link_option(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Option));
        }
    }

    /*class HighScore_Functions
    {
        public static async void Reset_Highscore()
        {
            StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.CreateFileAsync("HighScores.txt", CreationCollisionOption.OpenIfExists);
            await Windows.Storage.FileIO.WriteTextAsync(sampleFile, "0");
        }

        public static async void WriteStringToHSFile(string inp)
        {
            StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.CreateFileAsync("HighScores.txt", CreationCollisionOption.OpenIfExists);
            await Windows.Storage.FileIO.WriteTextAsync(sampleFile, inp);
        }

        public async static void Get_HS()
        {
            StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.CreateFileAsync("HighScores.txt", CreationCollisionOption.OpenIfExists);
            MainPage.High_Score = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
        }
    }*/
}
