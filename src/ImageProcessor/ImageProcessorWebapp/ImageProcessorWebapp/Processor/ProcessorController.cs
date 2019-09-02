using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ImageProcessorWebapp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImageProcessorWebapp.Processor
{
    public class ProcessorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string Image()
        {
            //todo: check query string is well formed

            var requestDictionary = QueryStringToDictionary(Request.QueryString.ToString());

            var imageRequest = ImageRequest.From(requestDictionary);

            return $"returning image after request with: {string.Join(", ", requestDictionary.Values)}";
        }

        private IDictionary<string, string> QueryStringToDictionary(string queryString)
        {
            var requestDictionary = new Dictionary<string, string>();

            var decoded = HttpUtility.UrlDecode(queryString);

            var sanitized = decoded.Replace("?", "");

            var splitQuery = sanitized.Split('&');

            foreach(var splitPart in splitQuery)
            {
                var values = splitPart.Split('=');

                var key = values[0].ToLower();
                var value = values[1];

                requestDictionary.Add(key, value);
            }

            return requestDictionary;
        }
    }
}