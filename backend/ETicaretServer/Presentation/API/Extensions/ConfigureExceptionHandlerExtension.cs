using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Extensions
{
    static public class ConfigureExceptionHandlerExtension
    {
        //Bu classta hataları özelleştireceğiz herşeyi try catch'e koymak yerine tek bir yerden kullanacağız ve hataları casuallaştıracağız 
        public static void ConfigureExceptionHandler<T>(this WebApplication application,ILogger<T> logger)
        {
            application.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = MediaTypeNames.Application.Json;

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    logger.LogError(contextFeature.Error.Message);
                       await context.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            Title = "Beklenmeyen bir hatayla karşılaşıldı!"
                        })); ;
                }
                });
            });
        }
    }
}
