using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Wordle.Api.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Wordle.Api.Data
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }
        public int GameNumber { get; set; }
        public string word { get; set; }
        public int turnNumber { get; set; }

    }
}
