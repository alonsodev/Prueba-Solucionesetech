using Solucionesetech.CrossCutting;
using Solucionesetech.CrossCutting.Common.Consts;
using Solucionesetech.Dtos.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace Solucionesetech.API.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizePermissionsAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public Permissions[] Permissions { get; set; }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //Validate if any permissions are passed when using attribute at controller or action level
            if (Permissions==null)
            {
                //Validation cannot take place without any permissions so returning unauthorized
                context.Result = new UnauthorizedResult();
                return;
            }
            if(Permissions.Count() == 0)
                return;

            //The below line can be used if you are reading permissions from token
            //var permissionsFromToken=context.HttpContext.User.Claims.Where(x=>x.Type=="Permissions").Select(x=>x.Value).ToList()

            //Identity.Name will have windows logged in user id, in case of Windows Authentication
            //Indentity.Name will have user name passed from token, in case of JWT Authenntication and having claim type "ClaimTypes.Name"


            var permissionsAsigned = Helper.GetPermissions(context.HttpContext.User);


            foreach (var x in Permissions)
            {
                if (permissionsAsigned.Contains((int)x))
                    return; //User Authorized. Wihtout setting any result value and just returning is sufficent for authorizing user
            }

            context.Result = new UnauthorizedResult();
            return;
        }

    }
}
