namespace BingoCoop
{
	public partial class Menu : Form
	{
		public Menu()
		{
			InitializeComponent();
		}

		private void joinBtn_Click(object sender, EventArgs e)
		{
			this.Hide();
			new Dialogue(false, this).Show();
		}

		private void hostBtn_Click(object sender, EventArgs e)
		{
			this.Hide();
			new Dialogue(true, this).Show();
		}
	}
}