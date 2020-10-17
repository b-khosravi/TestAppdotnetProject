
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApp.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;



namespace TestApp

{
[Route("/Error/")]
[ApiController]

 public class MyExceptionsHandler
 {
    private readonly RequestDelegate _next;
    private readonly ILogger<MyExceptionsHandler> _logger;
    private readonly IWebHostEnvironment _env;
  
    public MyExceptionsHandler(RequestDelegate next, ILogger<MyExceptionsHandler> logger, IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

      [HttpGet]
    public async Task Invoke(HttpContext context)
    {
        string message = null;
        HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);

            if (_env.IsDevelopment())
            {
                var dic = new Dictionary<string, string>
                {
                    ["StackTrace"] = ex.StackTrace,
                    ["Exception"] = ex.Message
                };
                message = JsonConvert.SerializeObject(dic);
            }
            else
            {
                message = "an error has occurred";
            }
            await WriteToReponseAsync();
        }
        async Task WriteToReponseAsync()
        {
            if (context.Response.HasStarted)
                throw new InvalidOperationException("The response has already started");
            var exceptionResult = new ExceptionResult(message, (int)httpStatusCode);
            var result = JsonConvert.SerializeObject(exceptionResult);
            context.Response.StatusCode = (int)httpStatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result);
        }
        
    }
 }
}
