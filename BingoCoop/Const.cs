using SuperSimpleTcp;
using System.Net;

namespace BingoCoop
{
	public static class Const
	{
		#region Set by app
		public static bool isDebugging = true;
		public static string logsFilePath;
		public static string logsFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
		public static string bingoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bingo.json");
		public static Form rootForm;
		public static bool hasRaisedExitError = false;

		public static bool isHosting;
		public static string IPv4 = GetIPv4();
		public static IPAddress serverIp;
		public static int serverPort;

		public static SimpleTcpServer server;
		public static SimpleTcpClient client;
		public static BingoSheet sheet;
		public static string buttonsContentJSON;
		public static bool hasReceivedBoard = false;
		#endregion

		#region Methods
		private static string GetIPv4()
		{
			string ipv4Address = string.Empty;
			string hostName = Dns.GetHostName();
			IPAddress[] addresses = Dns.GetHostAddresses(hostName);

			foreach (IPAddress address in addresses)
			{
				if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
				{
					ipv4Address = address.ToString();
					break;
				}
			}

			return ipv4Address;
		}
		#endregion
	}
}
