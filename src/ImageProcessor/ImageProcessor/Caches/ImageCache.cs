using ImageProcessor.Extensions;
using ImageProcessor.Layers;
using ImageProcessor.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;

namespace ImageProcessor.Caches
{
    public class ImageCache : IImageCache
    {
        private readonly ILogger logger;
        private readonly IMemoryCache memoryCache;
        private readonly IConfiguration configuration;
        private readonly string completeImageCacheKeyPrefix = "CompleteImage_";
        private readonly string imageLayerCacheKeyPrefix = "ImageLayer_";

        public ImageCache(ILogger<ImageCache> logger, IMemoryCache memoryCache, IConfiguration configuration)
        {
            this.logger = logger;
            this.memoryCache = memoryCache;
            this.configuration = configuration;
        }

        public Image<Rgba32> TryGetImage(MaltImage maltImage)
        {
            var completeImageKey = $"{completeImageCacheKeyPrefix}{maltImage.GetHashCode()}";

            if (!memoryCache.TryGetValue(completeImageKey, out Image<Rgba32> completeImageCacheEntry))
            {
                foreach(var layer in maltImage.ImageLayers)
                {
                    layer.LayerRender = TryGetLayer(layer);
                }

                completeImageCacheEntry = maltImage.ImageLayers.CombineLayers();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSize(1)
                    .SetSlidingExpiration(
                        TimeSpan.FromSeconds(
                            int.Parse(
                                configuration.GetSection("CacheOptions")["ImageExpiry"])));

                memoryCache.Set(completeImageKey, completeImageCacheEntry, cacheEntryOptions);

                logger.AddingItemToCache(nameof(maltImage), completeImageKey);
            }
            else
            {
                logger.RetrievedItemFromCache(completeImageKey);
            }

            return completeImageCacheEntry;
        }

        public Image<Rgba32> TryGetLayer(IImageLayer imageLayer)
        {
            var imageLayerKey = $"{imageLayerCacheKeyPrefix}{imageLayer.GetHashCode()}";

            if (!memoryCache.TryGetValue(imageLayerKey, out Image<Rgba32> imageLayerCacheEntry))
            {
                imageLayerCacheEntry = imageLayer.LayerRender;

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSize(1)
                    .SetSlidingExpiration(
                        TimeSpan.FromSeconds(
                            int.Parse(
                                configuration.GetSection("CacheOptions")["LayerExpiry"])));

                memoryCache.Set(imageLayerKey, imageLayerCacheEntry, cacheEntryOptions);

                logger.AddingItemToCache(nameof(imageLayer), imageLayerKey);
            }
            else
            {
                logger.RetrievedItemFromCache(imageLayerKey);
            }

            return imageLayerCacheEntry;
        }
    }
}
