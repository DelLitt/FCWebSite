namespace FCWeb.Core.Extensions.Middleware
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Middewares;

    public static class ImageProcessingMiddlewareExtensions
    {
        public static IApplicationBuilder UseImageProcessingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ImageProcessingMiddleware>();
        }
    }
}
