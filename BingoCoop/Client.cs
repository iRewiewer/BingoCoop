using Newtonsoft.Json;
using SuperSimpleTcp;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace BingoCoop
{
	public class Client
	{
		private int count = 0;
		public Client()
		{
			
		}

		#region Public methods
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

			return true;
		}
		#endregion
		#region Private methods
		private void Connected(object sender, ConnectionEventArgs e)
		{
			MessageBox.Show($"[Client] *** Server {e.IpPort} connected");
		}
		private void Disconnected(object sender, ConnectionEventArgs e)
		{
			MessageBox.Show($"[Client] *** Server {e.IpPort} disconnected");
		}
		private void DataReceived(object sender, DataReceivedEventArgs e)
		{
			string data = Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count);

			if (!Const.hasReceivedBoard)
			{
				Const.hasReceivedBoard = true;
				List<string> content = JsonConvert.DeserializeObject<List<string>>(data);
				Const.sheet.InitializeBoard(content);
			}
			else
			{
				Const.sheet.UpdateBoardOnClick(data);
			}
		}
		#endregion
		#region Utility methods
		#endregion
	}
}