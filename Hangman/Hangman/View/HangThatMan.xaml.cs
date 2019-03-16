using Hangman.Model;
using Hangman.ViewModel;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Hangman.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HangThatMan : Page
    {
        HangThatMan_ViewModel viewModel;
        public HangThatMan()
        {
            this.InitializeComponent();
            viewModel = new HangThatMan_ViewModel();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(Guess.Text)&&!string.IsNullOrWhiteSpace(Guess.Text))
            {
                MakeDecision(viewModel.Evaluate(Guess.Text));
            }

        }

        private void Guess_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            e.Handled = true;
            if (e.Key ==Windows.System.VirtualKey.Enter)
            {                
                if (!string.IsNullOrEmpty(Guess.Text) && !string.IsNullOrWhiteSpace(Guess.Text))
                {
                    MakeDecision(viewModel.Evaluate(Guess.Text));
                }
            }            
        }
        private async void MakeDecision(GameStatus input)
        {
            if(input == GameStatus.completematch)
            {
                viewModel.game.Win = true;
                App.CurrentGame = viewModel.game;
                Frame.Navigate(typeof(ResultPage));
            }
            else if(input==GameStatus.nomatch)
            {
                CurrentImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/"+viewModel.game.Tries+".jpg"));
                if (viewModel.game.Tries>=6)
                {
                    viewModel.game.Win = false;
                    App.CurrentGame = viewModel.game;
                    Frame.Navigate(typeof(ResultPage));
                }
            }
            else if(input==GameStatus.partialmatch)
            {
                if(viewModel.game.Guess.ToLower()==viewModel.game.Word.ToLower())
                {
                    viewModel.game.Win = true;
                    App.CurrentGame = viewModel.game;
                    Frame.Navigate(typeof(ResultPage));
                }
            }
            else if(input == GameStatus.used)
            {
                // Create the message dialog and set its content
                var messageDialog = new MessageDialog("That Character is already used", "Used Character");

                // Show the message dialog
                await messageDialog.ShowAsync();
            }
            Guess.Text = "";
        }
    }
}
