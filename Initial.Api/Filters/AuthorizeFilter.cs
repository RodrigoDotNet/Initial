using Initial.Api.Controllers.Templates;
using Initial.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Initial.Api.Filters
{
    public class AuthorizeFilter : Attribute, IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = 0;

        public AreaEnum? Area { get; }

        public ModeEnum? Mode { get; }

        public PolicyEnum? Policy { get; }

        public AuthorizeFilter()
        {
        }

        public AuthorizeFilter(AreaEnum area, ModeEnum mode)
        {
            Area = area;
            Mode = mode;
        }

        public AuthorizeFilter(PolicyEnum policy)
        {
            Policy = policy;
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context?.Controller as ControllerDefaultBase;

            if (controller != null
                && controller.AccountTicket != null)
            {
                if (Area == null && Mode == null && Policy == null)
                    return;

                if (Area != null && Mode != null)
                {
                    var mode = controller.AccountService
                        .GetAccessAreaMode(controller.AccountTicket, Area.Value)
                        .Result;

                    var all = Test(Mode.Value, mode);

                    if (all) return;
                }

                if (Policy != null)
                {
                    var has = controller.AccountService
                        .HasPolicyAccess(controller.AccountTicket, Policy.Value)
                        .Result;

                    if (has) return;
                }
            }

#warning Melhorar as mensagens de erro
            context.Result = new UnauthorizedResult();
        }

        public static bool Test(ModeEnum must, ModeEnum have)
        {
            foreach (Enum value in Enum.GetValues(typeof(ModeEnum)))
                if (must.HasFlag(value) && !have.HasFlag(value))
                    return false;

            return true;
        }
    }
}
