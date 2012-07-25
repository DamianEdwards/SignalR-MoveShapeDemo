using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SignalR.Hubs;

namespace MoveShape.Web.MoveShape
{
    [HubName("moveShape")]
    public class MoveShapeHub : Hub, IConnected, IDisconnect
    {
        private static readonly ConcurrentDictionary<string, object> _connections =
            new ConcurrentDictionary<string, object>();

        public void MoveShape(double x, double y)
        {
            Clients.shapeMoved(Context.ConnectionId, x, y);
        }

        public Task Connect()
        {
            _connections.TryAdd(Context.ConnectionId, null);
            return Clients.clientCountChanged(_connections.Count);
        }

        public Task Reconnect(IEnumerable<string> groups)
        {
            _connections.TryAdd(Context.ConnectionId, null);
            return Clients.clientCountChanged(_connections.Count);
        }

        public Task Disconnect()
        {
            object value;
            _connections.TryRemove(Context.ConnectionId, out value);
            return Clients.clientCountChanged(_connections.Count);
        }
    }
}