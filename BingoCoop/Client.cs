using System.Net;

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
		}
	}
}