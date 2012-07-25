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
            // Create an account on redistogo.com and put the details in here
            GlobalHost.DependencyResolver.UseRedis(
                server: "cod.redistogo.com",
                port: 1234,
                password: "password here",
                eventKey: "Codemania.MoveShape");
        }
    }
}