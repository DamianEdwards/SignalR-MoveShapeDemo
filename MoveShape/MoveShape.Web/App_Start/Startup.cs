using System.Web;
using SignalR;
using SignalR.Redis;

//[assembly:PreApplicationStartMethod(typeof(MoveShape.Web.App_Start.Startup), "Go")]

namespace MoveShape.Web.App_Start
{
    public static class Startup
    {
        public static void Go()
        {
            GlobalHost.DependencyResolver.UseRedis(
                server: "localhost",
                port: 6379,
                password: "",
                eventKey: "MoveShape");
        }
    }
}