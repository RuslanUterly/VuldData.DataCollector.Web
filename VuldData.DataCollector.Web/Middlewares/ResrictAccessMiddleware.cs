namespace VuldData.DataCollector.Web.Middlewares;

public class RestrictAccessMiddleware(RequestDelegate next) 
{
    public async Task InvokeAsync(HttpContext context)
    {
        var referrer = context.Request.Headers["Referrer"].FirstOrDefault();

        if (string.IsNullOrEmpty(referrer))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("403 Forbidden");
            return;
        }
        
        await next(context);
    }
}