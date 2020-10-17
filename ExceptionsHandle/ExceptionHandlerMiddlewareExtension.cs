using Microsoft.AspNetCore.Builder;
//using TestApp.Controllers;

/*namespace TestApp
{

    public static class ExceptionHandlerMiddlewareExtension
{
    public static void UseExceptionsHandlerController(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionsHandlerController>();
    }
}

}*/
namespace TestApp
{
    public static class MyExceptionsHandlerMiddlewareExtension
  {
     public static IApplicationBuilder UseMyExceptionsHandler(this IApplicationBuilder builder)
     {
        return  builder.UseMiddleware<MyExceptionsHandlerMidlleware>();

     }
  }
}
