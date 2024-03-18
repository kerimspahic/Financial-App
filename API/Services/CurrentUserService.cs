using Microsoft.AspNetCore.Mvc;

namespace API.Services
{
    public class CurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ActionResult<string> CurrentUser()
    {
        var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
        return userName;
    }
}
}