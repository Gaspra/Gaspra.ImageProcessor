using ImageProcessor.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace ImageProcessor.Extensions
{
    public static class LoggingExtensions
    {
        public static void BeginProcessingImageRequest(this ILogger logger, ImageRequest imageRequest)
        {
            var jsonImageRequest = JsonConvert.SerializeObject(imageRequest, Formatting.Indented);

            logger.LogDebug($"{LogPrefix}Executing image processing for request: {jsonImageRequest}");
        }

        public static void RetrievedItemFromCache(this ILogger logger, string itemKey)
        {
            logger.LogInformation($"{LogPrefix}Retrieved '{itemKey}' from the cache");
        }

        public static void AddingItemToCache(this ILogger logger, string item, string cacheKey)
        {
            logger.LogInformation($"{LogPrefix}Adding item '{item}' to cache with key '{cacheKey}'");
        }

        private static string LogPrefix => $"[{DateTimeOffset.UtcNow.ToString("dd:MM:yyyy HH:mm:ss.ffff")}]:";
    }
}
