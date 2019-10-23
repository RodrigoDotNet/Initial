using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Initial.Api.Filters
{
    /// <summary>
    /// Filter para ocultar ações não autenticadas.
    /// </summary>
    public class AuthorizeCheckOperationFilter : IOperationFilter, IDocumentFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            //if (context == null || operation == null) return;

            //if (!context.ApiDescription.TryGetMethodInfo(out MethodInfo methodInfo)) return;

            //var authorize = methodInfo
            //    .GetCustomAttributes(true)
            //    .Union(methodInfo.DeclaringType.CustomAttributes)
            //    .OfType<AuthorizeAttribute>()
            //    .Any();

            //if (!Thread.CurrentPrincipal.Identity.IsAuthenticated)
            //    if (authorize) operation.Tags?.Clear();

#warning Não implementado
        }
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)

        {
            //if (context == null || swaggerDoc == null) return;

            //if (!Thread.CurrentPrincipal.Identity.IsAuthenticated)
            //{
            //    foreach (var pathItem in swaggerDoc.Paths.Values)
            //    {
            //        pathItem.Delete = null;
            //        pathItem.Get = null;
            //        pathItem.Head = null;
            //        pathItem.Options = null;
            //        pathItem.Patch = null;
            //        pathItem.Post = null;
            //        pathItem.Put = null;
            //    }
            //}

#warning Não implementado
        }
    }
}
