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

        private readonly SWIGTYPE_p_WordSet WordSet = flwg_cs.init_WordSet(NumWords);

        private static readonly string DictionaryFilePath = "C:\\users\\jordan\\test-folder\\flwg\\docs\\4.txt";

        public string Error = "";
        public GameService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<string> StartGame() { 
                      
            flwg_cs.Initialize_HashMaps(WordToInt, IntToWord, DictionaryFilePath);
                      
            RandomNumberGenerator.Create();
           
            int initialWordId = RandomNumberGenerator.GetInt32(2233);
            
            string CurrWord = flwg_cs.Convert_IntToWord(initialWordId, IntToWord);

            return CurrWord; 


        }

        public async Task<string> AcceptInput(string input) {
            // Take the user input
            // Check if it is valid
            if (!IsUserInputValid(input)) {
                return "INVALID INPUT"; 
            }
            // Generate new word from it
            return ""; 
        
        }

        private bool IsUserInputValid(string? userInput)
        {
            if (userInput is null)
            {
                Error = "Input Cannot Be False";
                return false;
            }

            userInput = userInput.ToLower();

            if (userInput.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                Error = "Input Cannot Be A Special Character";
                return false;
            }

            if (userInput.Any(ch => char.IsDigit(ch)))
            {
                Error = "Input Cannot Contain A Number";
                return false;

            }

            if (userInput.Length != 4)
            {
                Error = "Input Must Be Four Characters";
                return false;

            }
            int wordID = flwg_cs.Convert_WordToInt(userInput, WordToInt);

            if (wordID == -1)
            {
                Error = "Word Not Found In Dictionary";
                return false;

            }



            if (flwg_cs.checkIfUsed_WordSet(wordID, WordSet) == 1)
            {
                Error = "Word Already Used";
                return false;
            }

            int commonLetters = 0;

            for (int i = 0; i < 4; i++)
            {
                commonLetters += (CurrWord[i] == userInput[i]) ? 1 : 0;
                Error += $"{CurrWord[i]} == {userInput[i]}\n";

            }
            if (commonLetters == 4)
            {
                Error = "Word Cannot Be The Same As Previous";
                return false;
            }
            if (commonLetters < 3)
            {
                Error += "Word must have at least 3 letters in common with previous";
                return false;
            }


            return true;



        }

    }
}
