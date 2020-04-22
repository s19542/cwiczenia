using cw.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace cwiczenia
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)//добавляє сервіси необхідні для роботи контролерів
        {
            services.AddSingleton<IDbService, MockDbService>();//додаємо власну базу, яка буде вставлена в конструктор Startup
            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();// Middleware 
            }

            // new Middleware
            app.UseRouting();//шукає контролер і методу для запиту

            // new Middleware
            app.UseAuthorization();//перевіряє забезпечення контролером

            app.UseMiddleware<CustomMiddleware>();//підключаємо наш CustomMiddleware до колейкі 

            // new Middleware3 
            //dodaje naglowek do odpowiedzi
            app.Use(async (context, c) =>
            {
                context.Response.Headers.Add("IndexNumber", "s19542");

                await c.Invoke();//передає естафету next  Middleware
            }
            );


            // new Middleware4
            //Middleware, який дозволяє запустити знайдені контролер і методу, які пов'язані з даним запитом HTTP 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGet("/", async context =>
                {
                    // отправка ответа в виде строки "Hello World!"
                    //Это выражение указывает, что для всех запросах по маршруту "/" 
                    //(то есть к корню веб-приложения) в ответ будет отправляться строка "Hello World!".
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
