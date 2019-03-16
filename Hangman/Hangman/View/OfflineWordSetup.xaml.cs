using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class OfflineWordSetup : Page
    {
        public OfflineWordSetup()
        {
            this.InitializeComponent();
            App.CurrentGame.Online = false;
        }

        private void Word_TextChanged(object sender, TextChangedEventArgs e)
        {
            App.CurrentGame.Word = ((TextBox)sender).Text;
            App.CurrentGame.Guess = new string(App.CurrentGame.Word.Select(x=> { if (x != ' ') { return '-'; } else { return ' '; } }).ToArray()) ;
           
        }

        private async void Hint_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            App.CurrentGame.Hint = Hint.Text;
            if (e.Key==Windows.System.VirtualKey.Enter)
            {
                if (String.IsNullOrEmpty(App.CurrentGame.Word) || String.IsNullOrWhiteSpace(App.CurrentGame.Word))
                {
                    // Create the message dialog and set its content
                    var messageDialog = new MessageDialog("You didn't come up with a word","Missing Word");

                    // Show the message dialog
                    await messageDialog.ShowAsync();

                    return;
                }

                if (String.IsNullOrEmpty(App.CurrentGame.Hint) || String.IsNullOrWhiteSpace(App.CurrentGame.Hint))
                {
                    // Create the message dialog and set its content
                    var messageDialog = new MessageDialog("You didn't come up with a hint", "Missing Word");

                    // Show the message dialog
                    await messageDialog.ShowAsync();

                    return;
                }                
                Frame.Navigate(typeof(HangThatMan));
            }
        }
    }
}
