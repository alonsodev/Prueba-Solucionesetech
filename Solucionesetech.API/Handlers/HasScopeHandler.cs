using Microsoft.AspNetCore.Authorization;
using Solucionesetech.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solucionesetech.API.Handlers
{
    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {
            // If user does not have the scope claim, get out of here
            if (!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == requirement.Issuer))
                return Task.CompletedTask;

            // Split the scopes string into an array
            var scopes = context.User.FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer).Value.Split(',');

            var requirement_scope_list = requirement.Scope.Split(',');

            // Succeed if the scope array contains the required scope
            if (scopes.Any(s => requirement_scope_list.Contains(s)))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
