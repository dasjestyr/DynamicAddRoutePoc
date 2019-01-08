using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MicrokernelLoading.Api.Controllers;
using MicrokernelLoading.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace MicrokernelLoading.Api
{
    public class DynamicControllerActivator : IControllerFactory
    {
        private readonly IServiceProvider _provider;
        private readonly ControllerRegistry _registry;

        public DynamicControllerActivator(ControllerRegistry registry, IServiceProvider provider)
        {
            _provider = provider;
            _registry = registry;

            
        }

        public object CreateController(ControllerContext context)
        {
            var controller = _registry.GetController(
                context.HttpContext.Request.Method,
                context.HttpContext.Request.Path);

            if (controller != null)
                return controller;
            
            context.HttpContext.Response.StatusCode = 404;
            return new DefaultController();
        }

        public void ReleaseController(ControllerContext context, object controller)
        {
            throw new NotImplementedException();
        }
    }

    public class ControllerRegistry
    {
        private readonly IServiceProvider _provider;
        private readonly RouteDefinitions _controllers = new RouteDefinitions();

        public ControllerRegistry(IServiceProvider provider)
        {
            _provider = provider;
        }

        public Controller GetController(string method, string route)
        {
            var requestMethod = new HttpMethod(method);
            var definition = _controllers.SingleOrDefault(c => c.Method == requestMethod && c.Route == route);
            if (definition == null) return null;

            var controller = _provider.GetService(Type.GetType(definition.ControllerName));
            return (Controller) controller;
        }

        public void AddRoute(ControllerRouteDefinition route)
        {
            if (!_controllers.Any(c => c.Method == route.Method && c.Route == route.Route))
                _controllers.AddRoute(route);
        }
    }
}
