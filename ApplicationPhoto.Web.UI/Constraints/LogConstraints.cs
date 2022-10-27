namespace ApplicationPhoto.Web.UI.Constraints
{
    public class LogConstraints : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (routeKey == @"\d+")
            {
                return true;
            }
            return false;
            
            
        }
    }
}
