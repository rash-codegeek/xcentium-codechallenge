using HtmlAgilityPack;

namespace ProjectDemo.Repository
{
    public class HtmlDocumentRepository : IHtmlDocumentRepository
    {
        private readonly HttpClient _httpClient;

        public HtmlDocumentRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<HtmlDocument> RetrieveHtmlContent(string url)
        {
            try
            {
                string htmlContent = await GetHtmlContent(url);

                // Create HtmlDocument and load HTML content
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(htmlContent);

                return htmlDocument;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving HTML document: {ex.Message}");
            }
        }
        private async Task<string> GetHtmlContent(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception($"Failed to retrieve HTML content. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving HTML content: {ex.Message}");
            }
        }
    }
}
