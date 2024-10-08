namespace BingoCoop
{
	public partial class Menu : Form
	{
		public Menu()
		{
			InitializeComponent();
			Const.rootForm = this;
			Const.logsFilePath = GetLogsFilePath();
			Log.AssureLogs(Const.logsFilePath);
			Log.Message("Starting application.");
			Log.Message($"Debugging is {(Const.isDebugging ? "enabled" : "disabled")}.");
		}

		private void joinBtn_Click(object sender, EventArgs e)
		{
			Const.hasRaisedExitError = false;
			Log.Message("Pressed join button.");
			this.Hide();
			Const.isHosting = false;
			new ConnectionDialogue(this).Show();
		}

		private void hostBtn_Click(object sender, EventArgs e)
		{
			Const.hasRaisedExitError = false;
			Log.Message("Pressed host button.");
			this.Hide();
			Const.isHosting = true;
			new ConnectionDialogue(this).Show();
		}

		private string GetLogsFilePath()
		{
			DateTime currentDateTime = DateTime.Now;
			string logFileName = $"Log_{currentDateTime:yyyy_MM_dd__HH_mm_ss}.txt";
			return Path.Combine(Const.logsFolderPath, logFileName);
		}
	}
}