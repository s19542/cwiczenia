using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cwiczenia
{
    public class CustomMiddleware
    {
        private RequestDelegate _next;
        public CustomMiddleware(RequestDelegate next)//kolejny middleware
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) {
            // if(context.Request.Headers.Keys...) первіряємо чи запит має такий заговолок якщо ні то мід просто викидує ексепшн і не передає естафету брату
            context.Response.Headers.Add("Cos", "w middleware");//dodajemy kolejny naglowek

            await _next.Invoke(context);//odpalamy kolejny middleware
        }
    }
}
