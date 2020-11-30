using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            //this should give us the user username from the token that api uses to authenticate the user
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}