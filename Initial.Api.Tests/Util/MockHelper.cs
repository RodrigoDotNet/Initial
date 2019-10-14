using Initial.Api.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using Moq;
using System.Collections.Generic;

namespace Initial.Api.Tests.Util
{
    public class MockHelper
    {
        /// <summary>
        /// Método utilizado para criar um mock do Localizer, utilizado no MVC
        /// </summary>
        public static IStringLocalizer<TController> Localizer<TController>()
        {
            var localizer = new Mock<IStringLocalizer<TController>>();

            localizer.Setup(_ => _[It.IsAny<string>()])
                .Returns((string key) =>
                {
                    var value = Messages.ResourceManager.GetString(key);

                    return new LocalizedString(key, value);
                });

            return localizer.Object;
        }

        /// <summary>
        /// Método utilizado para criar um mock do Localizer, utilizado no MVC
        /// </summary>
        public static IStringLocalizer Localizer()
        {
            var localizer = new Mock<IStringLocalizer>();

            localizer.Setup(_ => _[It.IsAny<string>()])
                .Returns((string key) =>
                {
                    var value = Messages.ResourceManager.GetString(key);

                    return new LocalizedString(key, value);
                });

            return localizer.Object;
        }

        public static ActionExecutingContext ActionExecutingContext
            (ControllerBase controller)
        {
            var modelState = new ModelStateDictionary();

            var actionContext = new ActionContext(
                Mock.Of<HttpContext>(),
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>(),
                modelState
            );

            return new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                controller
            );
        }
    }
}