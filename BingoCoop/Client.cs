using SuperSimpleTcp;
using System.Net;
using System.Text;
using BingoCoop;

namespace BingoCoop
{
	public class Client
	{

		private int count = 0;
		public Client()
		{
			
		}
		public bool Join(IPAddress ipAddress, int port)
		{
			Log.Message($"Joining server with ip: {ipAddress} and port: {port}");

			Const.client = new SimpleTcpClient($"{ipAddress}:{port}");

			Const.client.Events.Connected += Connected;
			Const.client.Events.Disconnected += Disconnected;
			Const.client.Events.DataReceived += DataReceived;

			try
			{
				Const.client.Connect();
			}
			catch (Exception ex)
			{
				return false;
			}
			
			//Const.client.Send("Hello, world!");
			return true;
		}
		static void Connected(object sender, ConnectionEventArgs e)
		{
			MessageBox.Show($"[Client] *** Server {e.IpPort} connected");
		}
		static void Disconnected(object sender, ConnectionEventArgs e)
		{
			MessageBox.Show($"[Client] *** Server {e.IpPort} disconnected");
		}
		private void DataReceived(object sender, DataReceivedEventArgs e)
		{
			//MessageBox.Show($"[Client] [{e.IpPort}] {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");

			//now when they receive data they just need to update with the string


			if(count == 0 && !Const.isHosting)
            {
				Const.sheet.UpdateMapClient(Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count));
            }

			else Const.sheet.UpdateColors(Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count));


			count++;
		}
	}
}