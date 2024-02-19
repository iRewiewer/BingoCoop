using SuperSimpleTcp;
using System.Net;
using System.Text;

namespace BingoCoop
{
	public class Server
	{
		public List<SimpleTcpClient> connectedClients;
		public List<string> connectedIP;

		public Server()
		{
			connectedClients = new List<SimpleTcpClient>();
			connectedIP = new List<string>();
		}

		#region Public methods
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

			return true;
		}
		#endregion
		#region Private methods
		private void ClientConnected(object sender, ConnectionEventArgs e)
		{
			MessageBox.Show($"[Server] [{e.IpPort}] client connected");

			SimpleTcpClient newClient = new SimpleTcpClient(e.IpPort);

			connectedClients.Add(newClient);
			connectedIP.Add(e.IpPort);

			if (Const.buttonsContentJSON != null)
			{
				Const.server.Send(e.IpPort, Const.buttonsContentJSON);
			}
		}
		private void ClientDisconnected(object sender, ConnectionEventArgs e)
		{
			MessageBox.Show($"[Server] [{e.IpPort}] client disconnected: {e.Reason}");

			connectedClients.RemoveAll(client => client.ServerIpPort == e.IpPort);
		}
		private void DataReceived(object sender, DataReceivedEventArgs e)
		{
			//MessageBox.Show($"[Server] [{e.IpPort}]: {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");

			//echo the received data back tot he client:
			//Const.server.Send(e.IpPort, "You said: " + Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count));


			//here he's gonna receive the data from client which means only a change which needs to be communicated to all other clients so

			//now send to all clients

			foreach (SimpleTcpClient client in connectedClients)
			{
				Const.server.Send(client.ServerIpPort, Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count));
			}
		}
		#endregion
		#region Utility methods
		#endregion
	}
}
