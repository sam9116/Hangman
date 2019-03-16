using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Model
{
    public class Game: INotifyPropertyChanged
    {
        public string Word =    "";
        private string guess = "";
        public string Guess
        {
            get
            {
                return guess;
            }
            set
            {
                guess = value;
                NotifyPropertyChanged("Guess");
            }
        }
        public string Hint =    "";
        public string UserName = "";
        public string UsedCharacter = "";
        public int Tries =  1;
        public bool Online =    false;
        public bool Win = false;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public enum GameStatus
    {
        completematch,partialmatch,nomatch,used
    }
}
