using Microsoft.AspNetCore.Mvc;
using ProjectDemo.Controllers;
using ProjectDemo.Repository;
using Moq;
using System;
using Castle.Core.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using ProjectDemo.Models;
using HtmlAgilityPack;
using System.Reflection;

namespace ProjectDemo.Test.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public async Task Test_FetchData()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<HomeController>>();
            var mockImageRepository = new Mock<IImageRepository>();
            var mockWordRepository = new Mock<IWordRepository>();
            var mockHtmlDocumentRepository = new Mock<IHtmlDocumentRepository>();
            var mockUrlValidationRepository = new Mock<IUrlValidationRepository>();
            var mockMemoryCache = new Mock<IMemoryCache>();

            var controller = new HomeController(mockLogger.Object, mockImageRepository.Object, mockWordRepository.Object,
                                                mockHtmlDocumentRepository.Object, mockUrlValidationRepository.Object,
                                                mockMemoryCache.Object);          
            //Mock Data
            var url = "";
            var inputModel1 = new InputDetails { InputUrl = url };
            var validUrl = "https://doc.sitecore.com/";
            var inputModel2 = new InputDetails { InputUrl = validUrl };

            mockUrlValidationRepository.Setup(repo => repo.ValidateUrl(validUrl)).Returns(true);

            // Act
            var result1 = await controller.FetchData(inputModel1) as PartialViewResult;
            var result2 = await controller.FetchData(inputModel2) as IActionResult;

            
            // Assert
            Assert.IsNull(result1);
            Assert.IsNotNull(result2);

            //Can add more testcases
        }
    }
}