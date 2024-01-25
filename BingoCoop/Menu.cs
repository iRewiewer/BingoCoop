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
			Log.Message($"Debugging is {(Const.debugging ? "enabled" : "disabled")}.");
		}

		private void joinBtn_Click(object sender, EventArgs e)
		{
			Log.Message("Pressed join button.");
			this.Hide();
			Const.isHosting = false;
			new Dialogue().Show();
		}

		private void hostBtn_Click(object sender, EventArgs e)
		{
			Log.Message("Pressed host button.");
			this.Hide();
			Const.isHosting = true;
			new Dialogue().Show();
		}

		private string GetLogsFilePath()
		{
			DateTime currentDateTime = DateTime.Now;
			string logFileName = $"Log_{currentDateTime:yyyy_MM_dd__HH_mm_ss}.txt";
			return Path.Combine(Const.logsFolderPath, logFileName);
		}
	}
}