using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace MoveShape.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Go();
        }

        private async void Go()
        {
            var hubConnection = new HubConnection("http://localhost:1235/");
            var hub = hubConnection.CreateHubProxy("moveShape");
            
            hub.On<double, double>("shapeMoved", (x, y) =>
                Dispatcher.InvokeAsync(() =>
                {
                    Canvas.SetLeft(Shape, (Body.ActualWidth - Shape.ActualWidth) * x);
                    Canvas.SetTop(Shape, (Body.ActualHeight - Shape.ActualHeight) * y);
                })
            );
            
            hub.On<int>("clientCountChanged", count =>
                Dispatcher.InvokeAsync(() =>
                    ClientCount.Text = count.ToString()));

            await hubConnection.Start();
            
            Shape.Draggable((left, top) =>
                hub.Invoke("MoveShape",
                    left / (Body.ActualWidth - Shape.ActualWidth),
                    top / (Body.ActualHeight - Shape.ActualHeight))
            );

            Closing += (_, __) => hubConnection.Stop();
        }
    }
}
