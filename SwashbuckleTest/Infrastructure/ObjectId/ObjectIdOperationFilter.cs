using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SwashbuckleTest.Infrastructure.ObjectId
{
    public class ObjectIdOperationFilter : IOperationFilter
    {
        private static readonly HashSet<string?> _patchmethods = new[] { HttpMethod.Get, HttpMethod.Delete }.Select(m => m.ToString()).ToHashSet<string?>(StringComparer.OrdinalIgnoreCase);

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var objectidparams = context.MethodInfo.GetParameters()
                .Select((param, index) => (param, index))
                .Where(pi => pi.param.ParameterType.Equals(typeof(ObjectId))).ToArray();

            if (_patchmethods.Contains(context.ApiDescription.HttpMethod) && objectidparams.Any())
                operation.RequestBody = null;

            foreach (var (param, index) in objectidparams)
            {
                var operationparam = operation.Parameters[index];
                operationparam.In = ParameterLocation.Query;
                operationparam.Schema = new OpenApiSchema()
                {
                    Type = "string",
                    Example = new OpenApiString(ObjectId.Empty)
                };
            }
        }
    }
}
