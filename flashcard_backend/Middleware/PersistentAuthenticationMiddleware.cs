using flashcard_backend.Interfaces;

namespace flashcard_backend.Middleware;

public class PersistentAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public PersistentAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, IAuthService authService)
    {
        if (!httpContext.User.Identity.IsAuthenticated)
        {
            if (httpContext.Request.Cookies.TryGetValue("PersistentToken", out string token))
            {
                var user = await authService.GetUserByPersistentToken(token);
                if (user != null)
                {
                    await authService.SignInUser(httpContext, user, true);
                }
            }
        }

        await _next(httpContext);
    }
    
}