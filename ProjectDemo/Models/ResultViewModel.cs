using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ProjectDemo.Models
{
    public class ResultViewModel
    {      
        public List<ImageGallery> ImageUrls { get; set; }
        public WordDetails WordCountDetails { get; set; }
    }
}
