using Hangman.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Hangman.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ResultPage : Page
    {
        ResultPage_ViewModel viewModel;
        public ResultPage()
        {
            this.InitializeComponent();
            viewModel = new ResultPage_ViewModel();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (App.CurrentGame.Win)
            {
                //upload game statics to unclesamscabin.ca
                if (App.CurrentGame.Online)
                {
                    string result = null;
                    string url = "http://hangmanleaderboards.unclesamscabin.ca/api/Players/" + App.CurrentGame.UserName;
                    WebResponse response = null;
                    StreamReader reader = null;

                    try
                    {
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                        request.Method = "GET";
                        response = await request.GetResponseAsync();
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
            
            base.OnNavigatedTo(e);
        }
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentGame = new Model.Game();
            Frame.Navigate(typeof(MainPage));
        }

        private async void ViewLeaderBoards_Click(object sender, RoutedEventArgs e)
        {
            // The URI to launch
            string uriToLaunch = @"http://unclesamscabin.ca/HangManLeaderBoards.html";

            // Create a Uri object from a URI string 
            var uri = new Uri(uriToLaunch);

            var success = await Windows.System.Launcher.LaunchUriAsync(uri);


        }
    }
}
