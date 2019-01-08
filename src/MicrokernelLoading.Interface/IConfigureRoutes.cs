using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace MicrokernelLoading.Interface
{
    public interface IConfigureRoutes
    {
        RouteDefinitions GetRoutes();
    }

    public class RouteDefinitions : IEnumerable<ControllerRouteDefinition>
    {
        private readonly List<ControllerRouteDefinition> _routes = new List<ControllerRouteDefinition>();
        
        public ControllerRouteDefinition AddRoute(HttpMethod method, string route, string controllerName)
        {
            if (this.Any(r => 
                r.Method == method &&  
                r.Route.Equals(route) || 
                r.ControllerName.Equals(controllerName)))
                throw new InvalidOperationException("route or controller already declared.");

            var controllerRoute = new ControllerRouteDefinition(method, route, controllerName);
            _routes.Add(controllerRoute);
            return controllerRoute;
        }

        public void AddRoute(ControllerRouteDefinition method)
        {
            AddRoute(method.Method, method.Route, method.ControllerName);
        }

        public IEnumerator<ControllerRouteDefinition> GetEnumerator()
        {
            return _routes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ControllerRouteDefinition : RouteDefinition
    {
        public RouteDefinition ExternalRoute { get; private set; }

        public string ControllerName { get; }

        public ControllerRouteDefinition(HttpMethod method, string route, string controllerName)
            : base(method, route)
        {
            ControllerName = controllerName;
        }

        public ControllerRouteDefinition WithExternalRoute(HttpMethod method, string route)
        {
            ExternalRoute = new RouteDefinition(method, route);
            return this;
        }
    }

    public class RouteDefinition
    {
        public HttpMethod Method { get; }

        public string Route { get; }

        public RouteDefinition(HttpMethod method, string route)
        {
            Method = method;
            Route = route;
        }
    }
}
