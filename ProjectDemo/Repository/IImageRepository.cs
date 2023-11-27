using HtmlAgilityPack;
using ProjectDemo.Models;

namespace ProjectDemo.Repository
{
    public interface IImageRepository
    {
        List<ImageGallery> GetImageDetails(HtmlDocument htmlDocument,string inputUrl);
    }
}
