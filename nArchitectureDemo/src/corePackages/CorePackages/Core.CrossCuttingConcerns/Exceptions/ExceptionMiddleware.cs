using Core.CrossCuttingConcerns.Exceptions.Handlers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HttpExceptionHandler _httpExceptionHandler;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
            _httpExceptionHandler = new HttpExceptionHandler();
        }

        public async Task Invoke(HttpContext context) //2-swaggeri islet
        {
            try
            {
                await _next(context);                 //3-meetodu calistir
            }
            catch (Exception exception)               //6-eger exception olarsa catch calistir
            {
                await HandleExceptionAsync(context.Response, exception);
            }

        }

        private Task HandleExceptionAsync(HttpResponse response, Exception exception) //8-swagerdeki responsu bu response ile deyis ve catch da return et
        {
            response.ContentType = "application/json";
            _httpExceptionHandler.Response = response;
            return _httpExceptionHandler.HandleExceptionAsync(exception);
        }
    }
}
