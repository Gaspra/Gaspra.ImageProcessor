using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;

namespace ImageProcessorWebapp.Extensions
{
    public static class LoggingExtensions
    {
        public static void ImageRequest(this ILogger logger, HttpRequest request)
        {
            logger.LogInformation($"{LogPrefix}Image requested with '{{queryString}}'", request.QueryString);
        }

        public static void BadImageRequest(this ILogger logger, HttpRequest request, Exception ex)
        {
            logger.LogError($"{LogPrefix}Image request: '{{queryString}}' failed due to: {{ex}}", request.QueryString, ex);
        }

        private static string LogPrefix => $"[{DateTimeOffset.UtcNow.ToString("dd:MM:yyyy HH:mm:ss.ffff")}]:";
    }
}
