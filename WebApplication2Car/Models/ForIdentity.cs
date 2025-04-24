using System.Security.Claims;

namespace WebApplication2Car.Models
{
    
    public static class ForIdentity
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            return principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
