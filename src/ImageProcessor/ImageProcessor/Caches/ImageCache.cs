using ImageProcessor.Extensions;
using ImageProcessor.Layers;
using ImageProcessor.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageProcessor.Caches
{
    public class ImageCache : IImageCache
    {
        private readonly ILogger Logger;
        private readonly IMemoryCache MemoryCache;
        private readonly string completeImageCacheKeyPrefix = "CompleteImage_";
        private readonly string imageLayerCacheKeyPrefix = "ImageLayer_";

        public ImageCache(ILogger<ImageCache> logger, IMemoryCache memoryCache)
        {
            Logger = logger;
            MemoryCache = memoryCache;
        }

        public Image<Rgba32> TryGetImage(MaltImage maltImage)
        {
            Logger.LogInformation($"Trying to get image with hash: {maltImage.GetHashCode()} from the cache");

            var completeImageKey = $"{completeImageCacheKeyPrefix}{maltImage.GetHashCode()}";

            if (!MemoryCache.TryGetValue(completeImageKey, out Image<Rgba32> completeImageCacheEntry))
            {
                foreach(var layer in maltImage.ImageLayers)
                {
                    layer.LayerRender = TryGetLayer(layer);
                }

                completeImageCacheEntry = maltImage.ImageLayers.CombineLayers();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));

                MemoryCache.Set(completeImageKey, completeImageCacheEntry, cacheEntryOptions);

                Logger.LogInformation($"Added image with hash: {maltImage.GetHashCode()} to the cache");
            }
            else
            {
                Logger.LogInformation($"Retrieved from the cache! malt image hash: {maltImage.GetHashCode()}");
            }

            return completeImageCacheEntry;
        }

        public Image<Rgba32> TryGetLayer(IImageLayer imageLayer)
        {
            var imageLayerKey = $"{imageLayerCacheKeyPrefix}{imageLayer.GetHashCode()}";

            Logger.LogInformation($"Trying to get image layer of type {imageLayer.GetType()} with hash: {imageLayer.GetHashCode()} from the cache");

            if (!MemoryCache.TryGetValue(imageLayerKey, out Image<Rgba32> imageLayerCacheEntry))
            {
                imageLayerCacheEntry = imageLayer.LayerRender;

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));

                MemoryCache.Set(imageLayerKey, imageLayerCacheEntry, cacheEntryOptions);

                Logger.LogInformation($"Added image layer of type {imageLayer.GetType()} with hash: {imageLayer.GetHashCode()} to the cache");
            }
            else
            {
                Logger.LogInformation($"Retrieved from the cache! Image layer of type {imageLayer.GetType()} with hash: {imageLayer.GetHashCode()}");
            }

            return imageLayerCacheEntry;
        }
    }
}
