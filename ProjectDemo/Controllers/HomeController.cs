using Microsoft.AspNetCore.Mvc;
using ProjectDemo.Models;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.AspNetCore.Http;
using static System.Net.WebRequestMethods;
using System.Xml;
using HtmlAgilityPack;
using ProjectDemo.Repository;
using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace ProjectDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IImageRepository _imageRepository;
        private readonly IWordRepository _wordRepository;
        private readonly IHtmlDocumentRepository _htmlDocumentRepository;
        private readonly IUrlValidationRepository _urlValidationRepository;
        private readonly IMemoryCache _cache;
        public HomeController(ILogger<HomeController> logger, IImageRepository imageRepository,
                              IWordRepository wordRepository, IHtmlDocumentRepository htmlDocumentRepository,
                              IUrlValidationRepository urlValidationRepository, IMemoryCache cache)
        {
            _logger = logger;
            _imageRepository = imageRepository;
            _wordRepository = wordRepository;
            _htmlDocumentRepository = htmlDocumentRepository;
            _urlValidationRepository = urlValidationRepository;
            _cache = cache;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Fetch Image and Word Details
        /// <summary>
        /// Get total number of words on the page,
        /// Display top 10 occurences &
        /// Fetch all images on the page asynchronously using HTML Agility Pack
        /// </summary>
        /// <param name="model">InputUrl entered by the user</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> FetchData(InputDetails model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (string.IsNullOrEmpty(model.InputUrl))
                {
                    ModelState.AddModelError("InputUrl", "Please provide a URL.");
                    return BadRequest(ModelState);
                }

                // Generate a unique cache key for the given input
                string cacheKey = $"URL_{model.InputUrl}";

                // Check if the data exists in the cache using the generated cache key
                if (!_cache.TryGetValue(model.InputUrl, out ResultViewModel cachedResult))
                {
                    string url = model.InputUrl;

                    //Validate input url
                    if (!_urlValidationRepository.ValidateUrl(url))
                    {
                        ModelState.AddModelError("InputUrl", "Please enter a valid URL.");
                        return BadRequest(ModelState);
                    }

                    //Retrive HTML Document content from the inputurl asynchronously using HTML Agility Pack
                    HtmlDocument htmlDocument = await _htmlDocumentRepository.RetrieveHtmlContent(url);
                    if (htmlDocument != null)
                    {
                        //Get Image Details
                        List<ImageGallery> imageUrls = _imageRepository.GetImageDetails(htmlDocument, url);

                        //Get Word Details
                        WordDetails wordCount = _wordRepository.GetWordDetails(htmlDocument, url);

                        // Pass result to the view model to retrieve on the view
                        cachedResult = new ResultViewModel
                        {
                            ImageUrls = imageUrls,
                            WordCountDetails = wordCount
                        };
                        _cache.Set(cacheKey, cachedResult, TimeSpan.FromMinutes(10));
                    }
                }
                if (cachedResult != null)
                {
                    return PartialView("Result", cachedResult);
                }
                else
                {
                    ModelState.AddModelError("InputUrl", "Failed to retrieve data for the provided URL.");
                    return BadRequest(ModelState);
                }


            }
            catch (Exception ex)
            {
                // Log the exception 
                _logger.LogError("Failed to retrieve data for the provided URL. " + ex.Message);
                ModelState.AddModelError("InputUrl", "Failed to retrieve data for the provided URL");
                return BadRequest(ModelState);

            }
        }
        #endregion
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult DisplayError()
        {
            return PartialView("ErrorView");
        }
    }
}