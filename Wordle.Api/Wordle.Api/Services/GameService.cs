using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using Wordle.Api.Data;
using Wordle.Api.Dtos;

namespace Wordle.Api.Services
{
    public class GameService
    {
        private readonly AppDbContext _db;

        private static readonly int NumWords = 2234;

        public bool HasData = false;

        public string CurrWord = string.Empty;

        public string? UserInput = "";

        private readonly SWIGTYPE_p_wordDataArray IntToWord = flwg_cs.Allocate_IntToWordStruct();

        private readonly SWIGTYPE_p_p_p_DummyHeadNode WordToInt = flwg_cs.Allocate_WordToInt();

        //private readonly SWIGTYPE_p_WordSet WordSet = flwg_cs.init_WordSet(NumWords);

        private static readonly string DictionaryFilePath = "C:\\users\\jordan\\test-folder\\flwg\\docs\\4.txt";

        public string Error = "";
        public GameService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<string> StartGame() { 

            // Generate the game id 

            // We initialize the HashMaps          
            flwg_cs.Initialize_HashMaps(WordToInt, IntToWord, DictionaryFilePath);
                      
            // We generate a random number generator
            RandomNumberGenerator.Create();
           
            // We pull a random number
            int initialWordId = RandomNumberGenerator.GetInt32(2233);

            int gameId = RandomNumberGenerator.GetInt32(100000);
            
            
            // We convert the random number to a string
            string CurrWord = flwg_cs.Convert_IntToWord(initialWordId, IntToWord);
            DataDto data = new()
            {
                GameId = gameId,
                Word = CurrWord
            };

            // Add word and id into Database
            Game turn = new()
            {
                GameNumber = gameId,
                turnNumber = 0,
                word = CurrWord
            };
            _db.Games.Add(turn);
            await _db.SaveChangesAsync(); 
           

            // Return the string which is the current 
            return JsonConvert.SerializeObject(data);


        }

        public async Task<string> AcceptInput(DataDto data) {
            
            // First we take the user input  
            string input = data.Word;
            
            // Then we take the current game id
            int gameId = data.GameId; 
            
            // Then we initialize the word set 
            SWIGTYPE_p_WordSet WordSet = flwg_cs.init_WordSet(NumWords);

            // Then we get all of the turns for a single game 
            IQueryable<Game> currGameTurns = _db.Games.Where(turn => turn.GameNumber == gameId);

            // Fill the Word Set
            foreach (Game turn in currGameTurns)
            {
                int w = flwg_cs.Convert_WordToInt(turn.word, WordToInt);
                flwg_cs.markUsed_WordSet(w, WordSet);
            }

            // Then we initialize the hash maps
            flwg_cs.Initialize_HashMaps(WordToInt, IntToWord, DictionaryFilePath);

            
            
            // We take the previous turn id, or the id of the most recent turn
            int prevTurnId = await currGameTurns.MaxAsync(turn => turn.turnNumber);
            
            // Then we take the object storing the word used in the previous turn 
            Game prevTurn = await currGameTurns.Where(turn => turn.turnNumber == prevTurnId).FirstAsync();

            // We get the previously used word
            string prevWord = prevTurn.word;

            // Take the user input
            // Check if it is valid
            string err; 

            if (!(err = IsUserInputValid(input, prevWord, WordSet)).Equals("valid")) {
                return JsonConvert.SerializeObject(new DataDto() { GameId = -1, Word = err });
            }

            // We convert the user's input to an integer 
            int wordId = flwg_cs.Convert_WordToInt(input, WordToInt);
            flwg_cs.markUsed_WordSet(wordId, WordSet);


            // Add the word to the database
            Game userTurn = new() {
                GameNumber = gameId,
                word = input,
                turnNumber = prevTurnId + 1
            };
            _db.Games.Add(userTurn);
            await _db.SaveChangesAsync();

           
            
       
            int newWordId = flwg_cs.botPly(wordId, 5, IntToWord, WordSet);

            if (newWordId == -1) {
                return JsonConvert.SerializeObject(new DataDto() { GameId = -1, Word = "Game Over" }); 
            }

            string botWord = flwg_cs.Convert_IntToWord(newWordId, IntToWord);

            // Add the new word to the database 
            Game botTurn = new()
            {
                GameNumber = gameId,
                word = botWord,
                turnNumber = prevTurnId + 2
            };
            _db.Games.Add(botTurn);
            await _db.SaveChangesAsync();

            DataDto dataOut = new()
            {
                GameId = gameId,
                Word = botWord
            };

            return JsonConvert.SerializeObject(dataOut);
             
        
        }

        private string IsUserInputValid(string? userInput, string prevWord, SWIGTYPE_p_WordSet WordSet)
        {

            if (userInput is null)
            {
                return "Input Cannot Be False";
                
            }

            userInput = userInput.ToLower();

            if (userInput.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                return "Input Cannot Be A Special Character";
                
            }

            if (userInput.Any(ch => char.IsDigit(ch)))
            {
                return "Input Cannot Contain A Number";
             

            }

            if (userInput.Length != 4)
            {
                return "Input Must Be Four Characters";
            

            }
            int wordID = flwg_cs.Convert_WordToInt(userInput, WordToInt);

            if (wordID == -1)
            {
                return "Word Not Found In Dictionary";
              

            }



            if (flwg_cs.checkIfUsed_WordSet(wordID, WordSet) == 1)
            {
                return "Word Already Used";
           
            }

            int commonLetters = 0;


            
            for (int i = 0; i < 4; i++)
            {
                commonLetters += (prevWord[i] == userInput[i]) ? 1 : 0;
                Error += $"{prevWord[i]} == {userInput[i]}\n";

            }
            if (commonLetters == 4)
            {
                return "Word Cannot Be The Same As Previous";
  
            }
            if (commonLetters < 3)
            {
                return "Word must have at least 3 letters in common with previous";
          
            }


            return "valid";



        }

    }
}
