using ALR.Data.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.Common.AuthorizationFilter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class SecurityPermissionAttribute : TypeFilterAttribute
    {
        public SecurityPermissionAttribute(params int[] role) : base(typeof(PermissionRequirementFilter))
        {
            Arguments = new object[] { role };
        }

    }

    public class PermissionRequirementFilter : IAuthorizationFilter
    {
        private readonly int[] _role;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionRequirementFilter(
            int[] role,
            ILogger<PermissionRequirementFilter> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _role = role;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
                if (allowAnonymous)
                {
                    // Skip authorization logic
                    return;
                }
                var userRoleClaim = _httpContextAccessor.HttpContext!.User.Claims.Where(c => c.Type.Equals(BaseConstants.USER_CLAIM_ROLE)).FirstOrDefault();
                if (userRoleClaim != null)
                {
                    var result = _role.Contains(Int32.Parse(userRoleClaim.Value));
                    if (result)
                        return;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in role authorization logic: {ex}");
            }

            context.Result = new ForbidResult();
        }
    }

}
