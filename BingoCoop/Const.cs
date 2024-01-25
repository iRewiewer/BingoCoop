using System.Net;

namespace BingoCoop
{
	public static class Const
	{
		public static readonly bool debugging = true;
		public static string logsFilePath;
		public static string logsFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
		public static string bingoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bingo.json");
		public static Form rootForm;

		public static bool isHosting;
		public static string IPv4 = GetIPv4();
		public static IPAddress serverIp;
		public static int serverPort;

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
	}
}
