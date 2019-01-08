using MicrokernelLoading.Interface;
using Module2.Controllers;
using System.Net.Http;

namespace Module2
{
    public class Installer : IConfigureRoutes
    {
        public RouteDefinitions GetRoutes()
        {
            var definitions = new RouteDefinitions();
            definitions.AddRoute(HttpMethod.Get, "foosvc/foo", typeof(BarController).AssemblyQualifiedName)
                .WithExternalRoute(HttpMethod.Get, "/foo");

            return definitions;
        }
    }
}
