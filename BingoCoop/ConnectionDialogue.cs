using System.Net;

namespace BingoCoop
{
	public partial class ConnectionDialogue : Form
	{
		private Form parentForm;
		private bool hasServerStarted = false;
		private bool isConnected = false;

		public ConnectionDialogue(Form parentForm)
		{
			this.parentForm = parentForm;

			InitializeComponent();

			if(Const.debugging)
			{
				this.portTBox.Text = "25565";
			}

			if (Const.isHosting)
			{
				this.ipLb.Name = "IPv4:";
				this.ipTBox.Enabled = false;
				this.ipTBox.Text = Const.IPv4;

				Const.serverIp = IPAddress.Parse(Const.IPv4);
			}
		}
		
		private void submitBtn_Click(object sender, EventArgs e)
		{
			if(Const.isHosting && !this.hasServerStarted)
			{
				#region Error Handling
				if (this.portTBox.Text.Trim() == string.Empty)
				{
					string errorMessage = "Port is empty. Please enter a valid port number.";
					Log.Warning(errorMessage, Log.WarningType.PortError);
					MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				if (!int.TryParse(portTBox.Text, out int _port))
				{
					string errorMessage = "Invalid port number. Please enter a valid port number.";
					Log.Warning(errorMessage, Log.WarningType.PortError);
					MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				#endregion

				Const.serverPort = _port;
				Server server = new Server();
				this.hasServerStarted = server.Start(Const.serverIp, Const.serverPort);
			}
			else
			{
				#region Error Handling
				if (!IPAddress.TryParse(this.ipTBox.Text, out IPAddress? _ip))
				{
					string errorMessage = "IP address is not valid. Please enter a valid IP.";
					Log.Warning(errorMessage, Log.WarningType.IPError);
					MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				if (this.ipTBox.Text.Trim() == string.Empty || _ip == null)
				{
					string errorMessage = "IP is empty. Please enter a valid IP.";
					Log.Warning(errorMessage, Log.WarningType.IPError);
					MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				if (!int.TryParse(portTBox.Text, out int _port))
				{
					string errorMessage = "Invalid port number. Please enter a valid port number.";
					Log.Warning(errorMessage, Log.WarningType.PortError);
					MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				if (this.portTBox.Text.Trim() == string.Empty)
				{
					string errorMessage = "Port is empty. Please enter a valid port number.";
					Log.Warning(errorMessage, Log.WarningType.PortError);
					MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				#endregion

				Const.serverIp = _ip;
				Const.serverPort = _port;
			}

			Client client = new Client();
			this.isConnected = client.Join(Const.serverIp, Const.serverPort);

			if (this.isConnected)
			{
				this.Hide();
				new BingoSheet(this).Show();
			}
		}

		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			Const.rootForm.Show();
		}
	}
}