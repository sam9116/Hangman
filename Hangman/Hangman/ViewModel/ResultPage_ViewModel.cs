using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Hangman.ViewModel
{
    public class ResultPage_ViewModel
    {
        public Visibility Win
        {
            get
            {
                if (App.CurrentGame.Win)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }
        public Visibility Lost
        {
            get
            {
                if (!App.CurrentGame.Win)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }
        public Visibility ShowOnline
        {
            get
            {
                if (App.CurrentGame.Online)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }
        public string UserName
        {
            get
            {
                return App.CurrentGame.UserName;
            }
        }
    }
}
