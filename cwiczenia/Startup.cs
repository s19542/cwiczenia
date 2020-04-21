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
        public void ConfigureServices(IServiceCollection services)//�������� ������ �������� ��� ������ ����������
        {
            services.AddSingleton<IDbService, MockDbService>();//������ ������ ����, ��� ���� ��������� � ����������� Startup
            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Middleware1
            app.UseRouting();//���� ��������� � ������ ��� ������

            //Middleware2
            app.UseAuthorization();//�������� ������������ �����������

            app.UseMiddleware<CustomMiddleware>();//��������� ��� CustomMiddleware �� ������ 

            //Middleware3 
            //dodaje naglowek do odpowiedzi
            app.Use(async (context, c) =>
            {
                context.Response.Headers.Add("Alisa zhgi", "s19542 gwiazdka");

                await c.Invoke();//������ �������� Middleware4
            }
            );


            //Middleware4
            //Middleware, ���� �������� ��������� ������� ��������� � ������, �� ���'���� � ����� ������� HTTP 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGet("/", async context =>
                {
                    // �������� ������ � ���� ������ "Hello World!"
                    //��� ��������� ���������, ��� ��� ���� �������� �� �������� "/" 
                    //(�� ���� � ����� ���-����������) � ����� ����� ������������ ������ "Hello World!".
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
