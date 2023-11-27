using HtmlAgilityPack;

namespace ProjectDemo.Repository
{
    public interface IHtmlDocumentRepository
    {
        Task<HtmlDocument> RetrieveHtmlContent(string url);
    }
}
