using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NguyenThanhLong_SE18C.NET_A01.Filters
{
    public class CustomAuthorizationFilter : IAuthorizationFilter
    {
        private readonly string[] _allowedRoles;

        public CustomAuthorizationFilter(params string[] allowedRoles)
        {
            _allowedRoles = allowedRoles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userRole = context.HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(userRole))
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            if (_allowedRoles.Length > 0 && !_allowedRoles.Contains(userRole))
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
                return;
            }
        }
    }

    public class AdminOnlyAttribute : TypeFilterAttribute
    {
        public AdminOnlyAttribute() : base(typeof(CustomAuthorizationFilter))
        {
            Arguments = new object[] { new[] { "0" } };
        }
    }

    public class StaffOnlyAttribute : TypeFilterAttribute
    {
        public StaffOnlyAttribute() : base(typeof(CustomAuthorizationFilter))
        {
            Arguments = new object[] { new[] { "1" } };
        }
    }

    public class AuthenticatedOnlyAttribute : TypeFilterAttribute
    {
        public AuthenticatedOnlyAttribute() : base(typeof(CustomAuthorizationFilter))
        {
            Arguments = new object[] { Array.Empty<string>() };
        }
    }
}
