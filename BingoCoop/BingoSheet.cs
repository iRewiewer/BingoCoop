using Newtonsoft.Json;

namespace BingoCoop
{
	public partial class BingoSheet : Form
	{
		private List<Button> buttonList = new List<Button>();

		public BingoSheet()
		{
			InitializeComponent();

			// Get list of buttons
			for (int i = 1; i <= 25; i++)
			{
				buttonList.Add((Button)this.Controls.Find($"Button{i}", true).FirstOrDefault());
			}

			RandomizeBoard();
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
			Const.rootForm.Show();
		}
	}
}
