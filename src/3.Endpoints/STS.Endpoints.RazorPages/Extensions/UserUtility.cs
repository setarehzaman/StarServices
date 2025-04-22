using System.Security.Claims;

namespace STS.Endpoints.RazorPages.Extensions
{
    public static class UserUtility
    {
        public static string GetRole(this ClaimsPrincipal user)
        {
            return user.Identities.FirstOrDefault()
                !.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user.Identities.FirstOrDefault()
                !.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        }

        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.Identities.FirstOrDefault()
                !.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        public static string GetClientId(this ClaimsPrincipal user)
        {
            return user.Identities.FirstOrDefault()
                !.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
        }
        public static string GetExpertId(this ClaimsPrincipal user)
        {
            return user.Identities.FirstOrDefault()
                !.Claims.FirstOrDefault(c => c.Type == "ExpertId")?.Value;
        }

        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.Identities.FirstOrDefault()
                !.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        }

    }
}
