﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using DotNetify;

namespace LiveChart
{
   public class Startup
   {
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddMemoryCache();
         services.AddSignalR();
         services.AddDotNetify();
      }

      public void Configure(IApplicationBuilder app)
      {
         app.UseStaticFiles();
         app.UseWebSockets();
         app.UseSignalR(routes => routes.MapDotNetifyHub());
         app.UseDotNetify();

         app.Run(async (context) =>
         {
            using (var reader = new StreamReader(File.OpenRead("wwwroot/index.html")))
               await context.Response.WriteAsync(reader.ReadToEnd());
         });
      }
   }
}