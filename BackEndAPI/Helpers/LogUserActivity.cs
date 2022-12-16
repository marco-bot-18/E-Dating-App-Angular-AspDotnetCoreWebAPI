using BackEndAPI.Extensions;
using BackEndAPI.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BackEndAPI.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated)
                return;

            var userId = resultContext.HttpContext.User.GetUserId();

            var repo = resultContext.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
            var user = await repo.GetUserByIdAysnc(userId);
            user.LastActive = DateTime.Now;
            await repo.SaveAllAsync();
        }
    }
}