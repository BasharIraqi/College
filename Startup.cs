using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace College
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "College", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.Use(async(context,next) =>
            //{
            //    if (context.Request.Path.Value.Contains("/students"))
            //    {
            //        await context.Response.WriteAsync("student" + context.Request.Path.Value.Replace("/students/"," ").Replace("/", " ").Remove(3));
            //    }
            //    await next();
            //});

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("class" + context.Request.Path.Value.Replace("/students/"," ").Remove(1,2));
            //});

            app.Map("/students/5", HandleClass);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "College v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void HandleClass(IApplicationBuilder obj)
        {
            obj.Use(async (context, next) =>
             {
                 await context.Response.WriteAsync("class 5 is amazing class");
             });
        }
        
        //public class Middlewares
        //{
        //    private readonly RequestDelegate _next;

        //    public Middlewares(RequestDelegate next)
        //    {
        //        _next = next;
        //    }

        //    public async Task Invoke(HttpContext context)
        //    {
        //        if (context.Request.Path.Value.Contains("/students"))
        //        {
        //            await context.Response.WriteAsync("student" + context.Request.Path.Value.Replace("/students/", " ").Replace("/", " ").Remove(3));
        //        }
        //        await _next(context);
        //    }
        //}
    }
}
