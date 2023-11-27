using HtmlAgilityPack;
using ProjectDemo.Models;

namespace ProjectDemo.Repository
{
    public interface IWordRepository
    {
        WordDetails GetWordDetails(HtmlDocument htmlDocument, string inputUrl);
    }
}
