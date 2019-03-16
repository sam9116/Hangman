using Hangman.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.ViewModel
{


    public class HangThatMan_ViewModel
    {
        public Game game = App.CurrentGame;

   
    
    
        public GameStatus Evaluate(string input)
        {            
            if(input.Count()>1)
            {
                if(input.ToLower()==game.Word.ToLower())
                {
                    return GameStatus.completematch;
                }
                else
                {
                    game.Tries++;
                    return GameStatus.nomatch;
                }
            }
            else
            {
                if(!game.UsedCharacter.Contains(input))
                {
                    char partial = input.ToCharArray()[0];
                    if (game.Word.Contains(input))
                    {
                        char[] partialword = game.Word.Select(x => { if (x != partial) { return '-'; } else { return x; } }).ToArray();

                        char[] guesscopy = game.Guess.ToCharArray();
                        for(int i=0;i< guesscopy.Count();i++)
                        {
                            if(guesscopy[i]=='-')
                            {
                                guesscopy[i] = partialword[i];
                            }
                        }
                        game.Guess = new string(guesscopy);

                        game.UsedCharacter += input;
                        return GameStatus.partialmatch;
                    }
                    else
                    {
                        game.UsedCharacter += input;
                        game.Tries++;
                        return GameStatus.nomatch;
                    }
                }
                else
                {
                    return GameStatus.used;
                }
                
            }

        }
    }
}
