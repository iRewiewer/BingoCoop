using SuperSimpleTcp;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BingoCoop
{
	public class Server
	{
		public List<TcpClient> connectedClients;
		public Server()
		{
			connectedClients = new List<TcpClient>();
		}
		public void Start(IPAddress ipAddress, int port)
		{
			Log.Message($"Starting server with ip: {ipAddress} and port: {port}");

			SimpleTcpServer server = new SimpleTcpServer($"{ipAddress}:{port}");

			server.Events.ClientConnected += ClientConnected;
			server.Events.ClientDisconnected += ClientDisconnected;
			server.Events.DataReceived += DataReceived;

			server.Start();

			server.Send("[ClientIp:Port]", "Hello, world!");
		}
		static void ClientConnected(object sender, ConnectionEventArgs e)
		{
			MessageBox.Show($"[Server] [{e.IpPort}] client connected");
		}
		static void ClientDisconnected(object sender, ConnectionEventArgs e)
		{
			MessageBox.Show($"[Server] [{e.IpPort}] client disconnected: {e.Reason}");
		}
		static void DataReceived(object sender, DataReceivedEventArgs e)
		{
			MessageBox.Show($"[Server] [{e.IpPort}]: {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");
		}
	}
}
