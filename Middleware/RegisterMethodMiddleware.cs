using Microsoft.AspNetCore.Http.Extensions;

class RegisterMethodMiddleware
{

    readonly RequestDelegate next;

    public RegisterMethodMiddleware(RequestDelegate nextRequest)
    {
        next = nextRequest;
    }

    public async Task Invoke(Microsoft.AspNetCore.Http.HttpContext context)
    {
        var startTime= DateTime.Now;

        await next(context);

        var endTime = DateTime.Now;

        
        var method = context.Request.Method;
        var url = context.Request.GetDisplayUrl();
        var elapsedTime= endTime-startTime;
        var status= context.Response.StatusCode;
        string statusMessaje= "Exitosa";

        if(status> 300){
            statusMessaje="Fallida";
        }
        

        System.Console.WriteLine($"Metodo: {method}, URL: {url}, Time: {elapsedTime.TotalSeconds:F3}, Code: {status} ({statusMessaje})");
    }


}


public static class RegisterMethodMiddlewareExtension
{

    public static IApplicationBuilder UseRegisterMethodMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RegisterMethodMiddleware>();
    }
}