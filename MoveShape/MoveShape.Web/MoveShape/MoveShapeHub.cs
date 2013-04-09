using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace MoveShape.Web.MoveShape
{
    [HubName("moveShape")]
    public class MoveShapeHub : Hub
    {
        private static readonly ConcurrentDictionary<string, object> _connections =
            new ConcurrentDictionary<string, object>();

        public void MoveShape(double x, double y)
        {
            Clients.Others.shapeMoved(x, y);
        }
        
        public override Task OnConnected()
        {
            _connections.TryAdd(Context.ConnectionId, null);
            return Clients.All.clientCountChanged(_connections.Count);
        }
        
        public override Task OnReconnected()
        {
            _connections.TryAdd(Context.ConnectionId, null);
            return Clients.All.clientCountChanged(_connections.Count);
        }

        public override Task OnDisconnected()
        {
            object value;
            _connections.TryRemove(Context.ConnectionId, out value);
            return Clients.All.clientCountChanged(_connections.Count);
        }
    }
}