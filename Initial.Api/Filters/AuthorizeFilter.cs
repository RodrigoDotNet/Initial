using Initial.Api.Controllers.Templates;
using Initial.Api.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Initial.Api.Filters
{
    public class AuthorizeFilter : Attribute, IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = 0;

        public AccessAreaEnum? Area { get; }

        public AccessModeEnum? Mode { get; }

        public AccessSpecialEnum? Special { get; }

        public AuthorizeFilter()
        {
        }

        public AuthorizeFilter(AccessAreaEnum area, AccessModeEnum mode)
        {
            Area = area;
            Mode = mode;
        }

        public AuthorizeFilter(AccessSpecialEnum special)
        {
            Special = special;
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as ControllerDefaultBase;

            if (controller != null
                && controller.AccountTicket != null)
            {
#warning Falta implementar verificações de acesso de Area, Mode, Special

                return;
            }

            context.Result = new UnauthorizedResult();
        }
    }
}
