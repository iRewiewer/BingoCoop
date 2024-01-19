using System.Net;
using System.Net.Sockets;

namespace BingoCoop
{
	public partial class Dialogue : Form
	{
		private bool isHosting = false;
		private Form parent;
		private int port;
		private IPAddress ipAddress;

		public Dialogue(bool isHosting, Form parent)
		{
			this.isHosting = isHosting;
			this.parent = parent;

			InitializeComponent();

			this.portTBox.Text = "25565";

			if (isHosting)
			{
				this.ipLb.Name = "IPv4:";
				this.ipTBox.Enabled = false;
				this.ipTBox.Text = GetIPv4();
				this.ipAddress = IPAddress.Parse(this.ipTBox.Text);
			}
		}
		
		private async void submitBtn_Click(object sender, EventArgs e)
		{
			if(isHosting)
			{
				if (this.portTBox.Text.Trim() == string.Empty)
				{
					MessageBox.Show("Port is empty. Please enter a valid port number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				if (!int.TryParse(portTBox.Text, out int _port))
				{
					MessageBox.Show("Invalid port number. Please enter a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				this.Hide();
				new BingoSheet().Show();

				this.port = _port;
				Server server = new Server();
				await server.Start(this.ipAddress, this.port);
			}
			else
			{
				if (!IPAddress.TryParse(this.ipTBox.Text, out IPAddress? _ip))
				{
					MessageBox.Show("IP address is not valid. Please enter a valid IP.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				if (this.ipTBox.Text.Trim() == string.Empty || _ip == null)
				{
					MessageBox.Show("IP is empty. Please enter a valid IP.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				if (!int.TryParse(this.portTBox.Text, out int _port))
				{
					MessageBox.Show("Invalid port number. Please enter a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				if (this.portTBox.Text.Trim() == string.Empty)
				{
					MessageBox.Show("Port is empty. Please enter a valid port number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				this.port = _port;
				this.ipAddress = _ip;
			}

			Client client = new Client();
			client.Join(this.ipAddress, this.port);

			this.Hide();
			new BingoSheet().Show();
		}

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
		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			parent.Show();
		}
	}
}