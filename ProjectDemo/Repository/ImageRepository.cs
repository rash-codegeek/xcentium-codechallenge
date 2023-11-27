using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectDemo.Models;
using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace ProjectDemo.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly ILogger<ImageRepository> _logger;

        public ImageRepository(ILogger<ImageRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #region ImageDetails
        /// <summary>
        /// Get images on the web page for the entered inputurl
        /// </summary>
        /// <param name="htmlDocument">html document to retrieve image tags</param>
        /// <param name="inputUrl">InputUrl entered by the user </param>
        /// <returns></returns>
        public List<ImageGallery> GetImageDetails(HtmlDocument htmlDocument,string inputUrl)
        {
            try
            {
                if (htmlDocument == null || string.IsNullOrEmpty(inputUrl))
                {
                    _logger.LogError("HTML document is null.");
                    return new List<ImageGallery>();
                }
                //select img tags from html document
                var imageElements = htmlDocument.DocumentNode.SelectNodes("//img");

                List<ImageGallery> imageUrls = new List<ImageGallery>();

                if (imageElements != null)
                {
                    foreach (var image in imageElements)
                    {
                        //Read src attribute value from the image tag
                        string imageUrl = image.GetAttributeValue("src", "");

                        //Add decoded image urls to the image list 
                        if (!string.IsNullOrWhiteSpace(imageUrl))
                        {
                            Uri baseUri = new Uri(inputUrl);
                            Uri imgUri = new Uri(baseUri, imageUrl);
                            imageUrls.Add(new ImageGallery { TargetUrl = HttpUtility.HtmlDecode(imgUri.AbsoluteUri) }); 
                        }                      
                    }
                }

                return imageUrls;
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError("Error fetching images: " + ex.Message);
                return new List<ImageGallery>(); 
            }
        }
    }
}
#endregion
