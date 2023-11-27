using HtmlAgilityPack;
using ProjectDemo.Models;
using System.Net;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ProjectDemo.Repository
{
    public class WordRepository : IWordRepository
    {
        private readonly ILogger<WordRepository> _logger;

        public WordRepository(ILogger<WordRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #region WordDetails
        /// <summary>
        /// get total number of words on the page and display top 10 occurences.
        /// </summary>
        /// <param name="htmlDocument">html document to retrieve word details</param>
        /// <param name="inputUrl">InputUrl entered by the user </param>
        /// <returns></returns>
        public WordDetails GetWordDetails(HtmlDocument htmlDocument, string inputUrl)
        {
            var wordDetails = new WordDetails();
            if (htmlDocument == null || string.IsNullOrEmpty(inputUrl))
            {
                _logger.LogError("HTML document is null.");
                return wordDetails;
            }
            
            try
            {

                // Extract text from HTML content
                string decodedContent = WebUtility.HtmlDecode(htmlDocument.DocumentNode.InnerText);

                // Split the text into words
                string pattern = @"\b(?![0-9]+\b)\w+\b";

                MatchCollection matches = Regex.Matches(decodedContent, pattern);
                Dictionary<string, int> wordCountDictionary = new Dictionary<string, int>();
                foreach (Match match in matches)
                {
                    string word = match.Value.Trim().ToLower();
                    if (!wordCountDictionary.ContainsKey(word))
                    {
                        wordCountDictionary[word] = 1;
                    }
                    else
                    {
                        wordCountDictionary[word]++;
                    }
                }
                // Count total words
                wordDetails.TotalWordsCount = wordCountDictionary.Sum(pair => pair.Value);
                // Calculate word frequency
                wordDetails.TopWords = wordCountDictionary
                                .Where(pair => !pair.Key.Any(char.IsDigit)) 
                                .OrderByDescending(pair => pair.Value)
                                .Take(10).ToDictionary(pair => pair.Key, pair => pair.Value);
                return wordDetails;
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError("An error occurred: " + ex.Message);
                return wordDetails;
            }
        }
    }
}
#endregion 