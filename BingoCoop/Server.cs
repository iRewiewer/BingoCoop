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
		public bool Start(IPAddress ipAddress, int port)
		{
			Log.Message($"Starting server with ip: {ipAddress} and port: {port}");

			Const.server = new SimpleTcpServer($"{ipAddress}:{port}");

			Const.server.Events.ClientConnected += ClientConnected;
			Const.server.Events.ClientDisconnected += ClientDisconnected;
			Const.server.Events.DataReceived += DataReceived;

			try
			{
				Const.server.Start();
			}
			catch (Exception ex)
			{
				return false;
			}

			Const.server.Send("[ClientIp:Port]", "Hello, world!");
			return true;
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
