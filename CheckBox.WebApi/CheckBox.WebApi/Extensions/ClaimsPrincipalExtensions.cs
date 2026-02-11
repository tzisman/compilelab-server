using System.Security.Claims;

namespace CheckBox.WebApi.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int? GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userIdClaim, out int id))
            {
                return id;
            }

            return null;
        }
    }
}
