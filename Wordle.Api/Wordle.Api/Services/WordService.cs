using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using Wordle.Api.Data;

namespace Wordle.Api.Services
{
    public class WordService
    {
        private readonly AppDbContext _db;
        private static readonly object _WordOfTheDayLock = new object();

        public WordService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<string> GetWordOfDayLastTenDays() {
            List<string> wordOfDayTenDays = new ();  
            for (int i = 0; i > -10; i--) {
                wordOfDayTenDays.Add(await GetWordOfDay(DateTime.Now.AddDays(i)));


            }
            return JsonConvert.SerializeObject(wordOfDayTenDays); 
        }

        public async Task<string> GetWordOfDay(DateTime date) {
            // Get the current date
            DateTime currentDate = date;
            int day = currentDate.Day;
            int month = currentDate.Month;
            int year = currentDate.Year;

            // Convert to a string
            string dateString = string.Format("{0}/{1}/{2}", year, day, month);
            // Hash the string
            int hashCode; 
            using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(dateString));
                hashCode = BitConverter.ToInt32(hashBytes, 0);
   
            }
            // Mod by the number of words 
            var count = await _db.Words.CountAsync(word => word.IsCommon);
            int index = Math.Abs(hashCode % count);
            // Return the word as text
            return (await _db.Words
                .Where(word => word.IsCommon)
                .Skip(index)
                .FirstAsync()).Text;
            

        }

        public async Task<string> GetRandomWord()
        {
            var count = await _db.Words.CountAsync(word => word.IsCommon);
            var index = new Random().Next(count);
            var word = await _db.Words
                .Where(word => word.IsCommon)
                .Skip(index)
                .FirstAsync();
            return word;
        }

        public async Task<IEnumerable<Word>> GetSeveralWords(int? count)
        {
            count ??= 10;
            var totalCount = await _db.Words.CountAsync(word => word.IsCommon);
            totalCount -= count.Value;
            int index = new Random().Next(totalCount);
            var words = await _db.Words
                .Where(word => word.IsCommon)
                .Skip(index)
                .Take(count.Value)
                .OrderByDescending(w => w.Text)
                .ToListAsync();
            return words;
        }

        public async Task<Word> AddWord(string? newWord, bool isCommon)
        {
            if (newWord is null || newWord.Length != 5)
            {
                throw new ArgumentException("Word must be 5 characters long");
            }
            var word = await _db.Words.FirstOrDefaultAsync(w => w.Text == newWord);
            if (word != null)
            {
                word.IsCommon = isCommon;
            }
            else
            {
                word = new()
                {
                    Text = newWord,
                    IsCommon = isCommon
                };
                _db.Words.Add(word);
            }
            await _db.SaveChangesAsync();
            return word;
        }

        public async Task<string> GetWordOfTheDay(TimeSpan offset, DateTime? date = null)
        {
            if (date is null)
            {
                date = DateTime.UtcNow;
            }
            
            var localDateTime = new DateTimeOffset(date.Value.Ticks, offset);
            var localDate = localDateTime.Date;
            var todaysWord = await _db.DateWords
                .Include(f => f.Word)
                .FirstOrDefaultAsync(f => f.Date == localDate);

            if (todaysWord != null)
            {
                return todaysWord.Word.Text;
            } else
            {
                lock (_WordOfTheDayLock)
                {
                    var todaysLatestWord = _db.DateWords
                        .Include(f => f.Word)
                        .FirstOrDefault(f => f.Date == localDate);

                    if (todaysLatestWord != null)
                    {
                        return todaysLatestWord.Word.Text;
                    }
                    var word = GetRandomWord().Result;

                    var dateWord = new DateWord
                    {
                        Date = localDate,
                        Word = word
                    };
                    _db.DateWords.Add(dateWord);
                    try
                    {
                        _db.SaveChanges();
                    }
                    catch(SqlException e) // this is probably not the right error to catch
                    {
                        if (e.Message.Contains("duplicate"))
                        {
                            return _db.DateWords
                                .Include(f => f.Word)
                                .First(f => f.Date == localDate)
                                .Word.Text;
                        }
                    }
                    return word.Text;
                }
            }
        }
    }
}