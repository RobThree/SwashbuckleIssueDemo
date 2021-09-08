using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Globalization;

namespace SwashbuckleTest.Infrastructure.ObjectId
{
    public class ObjectIdConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.TryGetValue(routeKey, out var value))
                return ObjectId.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture), out var _);

            return false;
        }
    }
}
