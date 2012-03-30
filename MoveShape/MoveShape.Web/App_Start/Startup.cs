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
            Global.DependencyResolver.UseRedis(
                server: "cod.redistogo.com",
                port: 9053,
                password: "73e01e6bb0dff0d3136953917a6613a0",
                eventKey: "Codemania.MoveShape");
        }
    }
}