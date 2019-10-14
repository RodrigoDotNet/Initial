using Initial.Api.Controllers.Templates;
using Initial.Api.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Security.Claims;

namespace Initial.Api.Filters
{
    public class AccountTicketBinderFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = 0;

        public AccountTicketBinderFilter(IAccountService service = null)
        {
            Service = service;
        }

        public IAccountService Service { get; }

        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (Service == null
                || !(context.Controller is ControllerDefaultBase))
                return;

            var controller = context.Controller as ControllerDefaultBase;

            controller.AccountService = Service;

            var publicId = context.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Sid)
                ?.Value;

            if (publicId != null
                && Guid.TryParse(publicId, out Guid publicGuid)
                && Service.IsValid(publicGuid, out AccountTicket ticket))
            {
                controller.AccountTicket = ticket;

                return;
            }
        }
    }
}
