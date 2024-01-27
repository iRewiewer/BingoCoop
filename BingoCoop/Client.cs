using SuperSimpleTcp;
using System.Net;
using System.Text;

namespace BingoCoop
{
	public class Client
	{
		public Client()
		{
			
		}
		public void Join(IPAddress ipAddress, int port)
		{
			Log.Message($"Joining server with ip: {ipAddress} and port: {port}");

			SimpleTcpClient client = new SimpleTcpClient($"{ipAddress}:{port}");

			client.Events.Connected += Connected;
			client.Events.Disconnected += Disconnected;
			client.Events.DataReceived += DataReceived;

			client.Connect();

			client.Send("Hello, world!");
		}
		static void Connected(object sender, ConnectionEventArgs e)
		{
			MessageBox.Show($"[Client] *** Server {e.IpPort} connected");
		}
		static void Disconnected(object sender, ConnectionEventArgs e)
		{
			MessageBox.Show($"[Client] *** Server {e.IpPort} disconnected");
		}
		static void DataReceived(object sender, DataReceivedEventArgs e)
		{
			MessageBox.Show($"[Client] [{e.IpPort}] {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");
		}
	}
}