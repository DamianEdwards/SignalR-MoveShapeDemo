using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace MoveShape.Web
{
    public static class SignalRConfig
    {
        public static void ConfigureSignalR(RouteCollection routes, IDependencyResolver dependencyResolver)
        {
            routes.MapHubs();
        }
    }
}