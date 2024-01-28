using Newtonsoft.Json;
using SuperSimpleTcp;

namespace BingoCoop
{
	public partial class BingoSheet : Form
	{
		private List<Button> buttonList = new List<Button>();
		private Form parentForm;
		private bool hasSetName = false;
		private bool hasSetColor = false;

		public BingoSheet(Form parentForm)
		{
			this.parentForm = parentForm;

			InitializeComponent();

			// Get list of buttons
			for (int i = 1; i <= 25; i++)
			{
				buttonList.Add((Button)this.Controls.Find($"Button{i}", true).FirstOrDefault());
			}

			RandomizeBoard();
		}
		private void colorBtn_Click(object sender, EventArgs e)
		{
			if (colorDialog.ShowDialog() != DialogResult.Cancel)
			{
				colorBtn.ForeColor = colorDialog.Color;
				colorBtn.BackColor = colorDialog.Color;
			}
			this.hasSetColor = true;

			if (this.hasSetColor && this.hasSetName)
			{
				buttonList.ForEach(button => { button.Enabled = true; });
			}
		}
		private void playerNameBtn_Click(object sender, EventArgs e)
		{
			PlayerNameDialogue playerNameDialogue = new PlayerNameDialogue();
			if (playerNameDialogue.ShowDialog() == DialogResult.OK)
			{
				playerNameBtn.Text = playerNameDialogue.playerName;
			};
			this.hasSetName = true;

			if (this.hasSetColor && this.hasSetName)
			{
				buttonList.ForEach(button => {
					button.Enabled = true;
					button.BackColor = Color.White;
				});
			}
		}
		private void BingoButton_Click(object sender, EventArgs e)
		{
			Button buttonPressed = (Button)sender;

			// send info to server that button has been clicked
			// mark it with color of player
		}
		private void RandomizeBoard()
		{
			string jsonContent = File.ReadAllText(Const.bingoPath);

			Dictionary<string, List<int>> bingoDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(jsonContent);

			Random random = new Random();
			List<string> usedEntries = new List<string>();

			foreach (Button button in this.buttonList)
			{
				// Get a random entry from the dictionary
				KeyValuePair<string, List<int>> randomEntry;
				do
				{
					randomEntry = bingoDictionary.ElementAt(random.Next(bingoDictionary.Count));
				} while (usedEntries.Contains(randomEntry.Key));

				// Set the formatted text of the button
				string buttonText = randomEntry.Key;

				// Replace "_" with a random value from the List<int> if it's not "null"
				if (randomEntry.Value != null)
				{
					int randomValue = randomEntry.Value[random.Next(randomEntry.Value.Count)];
					buttonText = buttonText.Replace("_", randomValue.ToString());
				}

				// Additional formatting (modify as needed)
				buttonText = buttonText.Replace("{", "").Replace("}", "").Replace("|", "");

				button.Text = buttonText;

				// Mark the used entry to ensure uniqueness
				usedEntries.Add(randomEntry.Key);
			}
		}
		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			Log.Message("Closing bingo sheet.");
			if (Const.isHosting)
			{
				Const.client.Disconnect();
				if (Const.server != null)
				{
					Const.server.Stop();
				}
			}

			Const.rootForm.Show();
			this.parentForm.Close();
		}
	}
}
