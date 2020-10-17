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
    public static class ExceptionsHandlerMiddlewareExtension
  {
     public static IApplicationBuilder UseMyExceptionsHandler(this ApplicationBuilder builder)
     {
        return  builder.UseMiddleware<MyExceptionsHandler>();

     }
  }
}
