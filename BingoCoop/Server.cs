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
		public async Task Start(IPAddress ipAddress, int port)
		{
			try
			{
				TcpListener tcpListener = new TcpListener(ipAddress, port);
				tcpListener.Start();

				/*while (true)
				{
					TcpClient client = tcpListener.AcceptTcpClient();

					Console.WriteLine($"Client connected: {((IPEndPoint)client.Client.RemoteEndPoint).Address}");

					connectedClients.Add(client);
					//Thread clientThread = new Thread(() => HandleClientCommunication(client));
					//clientThread.Start();
				}*/
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
			}
		}
		private void HandleClientCommunication(TcpClient client)
		{
			try
			{
				NetworkStream stream = client.GetStream();

				using (StreamReader reader = new StreamReader(stream))
				{
					using (StreamWriter writer = new StreamWriter(stream))
					{
						while (true)
						{
							// Read the JSON message from the client
							string jsonMessage = reader.ReadLine();

							// Broadcast the message to other clients
							BroadcastMessage(jsonMessage, client);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error handling client communication: {ex.Message}");
			}
		}

		private void BroadcastMessage(string message, TcpClient senderClient)
		{
			// Loop through all connected clients and send the message
			foreach (TcpClient client in connectedClients)
			{
				if (client != senderClient)
				{
					// Get the network stream for writing
					NetworkStream stream = client.GetStream();

					// Create a StreamWriter for writing to the network stream
					using (StreamWriter writer = new StreamWriter(stream))
					{
						// Write the message to the network stream
						writer.WriteLine(message);
						writer.Flush();
					}
				}
			}
		}
	}
}
