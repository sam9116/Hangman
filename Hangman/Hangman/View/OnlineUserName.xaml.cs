using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class OnlineUserName : Page
    {
        public OnlineUserName()
        {
            this.InitializeComponent();
            App.CurrentGame.Online = true;
        }

        private void PlayerName_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            e.Handled = true;
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                if(!string.IsNullOrEmpty(PlayerName.Text)&&!string.IsNullOrWhiteSpace(PlayerName.Text))
                {
                    SetupUserName(PlayerName.Text);
                    SetUpOnlineGame();
                }
               
            }
        }

        private void SetupUserName(string input)
        {
            App.CurrentGame.UserName = input;


           /* string url = "http://hangmanleaderboards.unclesamscabin.ca/api/Players/" + App.CurrentGame.UserName;
            WebResponse response = null;


            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                response = await request.GetResponseAsync();
            }
            catch (Exception ex)
            {
                var messageDialog = new MessageDialog("Something failed, switching to Offline Mode", "Exception");

                // Show the message dialog
                await messageDialog.ShowAsync();

                Frame.Navigate(typeof(OfflineWordSetup));
            }*/
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PlayerName.Text) && !string.IsNullOrWhiteSpace(PlayerName.Text))
            {
                SetupUserName(PlayerName.Text);
                SetUpOnlineGame();
            }
        }

        private async void SetUpOnlineGame()
        {
            //now I need to crawl randomword.com to steal their word and definitions 

            string result = null;
            string url = "http://www.randomword.com";
            WebResponse response = null;
            StreamReader reader = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                response = await request.GetResponseAsync();
                reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = reader.ReadToEnd();
                string[] seperator = new string[] { "<div id=\"random_word\">", "<div id=\"random_word_definition\">", "</div>" };
                string[] seperated_result = result.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                App.CurrentGame.Word = seperated_result[3];
                App.CurrentGame.Hint = seperated_result[5];
                App.CurrentGame.Guess = new string(App.CurrentGame.Word.Select(x => { if (x != ' ') { return '-'; } else { return ' '; } }).ToArray());
                Frame.Navigate(typeof(HangThatMan));
            }
            catch (Exception ex)
            {
                var messageDialog = new MessageDialog("Something failed, switching to Offline Mode", "Exception");

                // Show the message dialog
                await messageDialog.ShowAsync();

                Frame.Navigate(typeof(OfflineWordSetup));

            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
                if (response != null)
                    response.Dispose();
            }
        }
    }
}
