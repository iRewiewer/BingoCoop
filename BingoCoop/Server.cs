using System.Net;
using System.Net.Sockets;

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

			try
			{
				TcpListener tcpListener = new TcpListener(ipAddress, port);
				tcpListener.Start();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
			}
		}
	}
}
