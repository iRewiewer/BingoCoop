using Newtonsoft.Json;
using SuperSimpleTcp;

namespace BingoCoop
{
	public partial class BingoSheet : Form
	{
		private List<Button> buttonsList = new List<Button>();
		private List<string> buttonsContentList = new List<string>();
		private Form parentForm;
		private bool hasSetName = false;
		private bool hasSetColor = false;
		private Color playerColor;

		public BingoSheet(Form parentForm)
		{
			this.parentForm = parentForm;

			InitializeComponent();

			// Get list of buttons
			for (int i = 1; i <= 25; i++)
			{
				Button currentButton = (Button)this.Controls.Find($"Button{i}", true).FirstOrDefault();
				buttonsList.Add(currentButton);
				buttonsContentList.Add(currentButton.Text);
			}

			Const.buttonsContentJSON = JsonConvert.SerializeObject(buttonsContentList);
			RandomizeBoard();
		}
		#region Public methods
		public void UpdateColors(string data)
		{
			//i receive data and i update the colors + disable the thing

			//let's parse string which will be button:color:true/false

			Console.WriteLine(data);

			// Split the input string into parts based on ":" and remove any surrounding spaces
			string[] parts = data.Split(':');

			// Extract the button name (first part)
			string buttonText = parts[0].Trim();

			// Extract the color string (second part)
			string colorString = parts[1].Trim().Substring("Color [".Length); // Remove "Color [" from the beginning
			colorString = colorString.Remove(colorString.Length - 1); // Remove "]" from the end

			// Extract the boolean value (third part)
			bool booleanValue = Convert.ToBoolean(parts[2].Trim());

			// Parse the color string to get R, G, and B values
			int r = int.Parse(colorString.Split(',')[1].Trim().Substring(2));
			int g = int.Parse(colorString.Split(',')[2].Trim().Substring(2));
			int b = int.Parse(colorString.Split(',')[3].Trim().Substring(2));

			// Create Color object using the extracted R, G, B values
			Color color = Color.FromArgb(r, g, b);


			//good now we have all we need so let's find the buttona and update 

			this.Invoke((MethodInvoker)(() =>

			{
				foreach (Button button in buttonsList)
				{
					if (button.Text == buttonText)
					{
						button.Enabled = booleanValue;
						button.BackColor = color;
						break;
					}
				}
			}
			));
		}
		public void UpdateMapClient(string data)
		{
			string[] parts = data.Split(':');

			int i = 0;

			this.Invoke((MethodInvoker)(() =>
			{
				foreach (Button button in buttonsList)
				{
					button.Text = parts[i];
					i++;
				}
			}));
		}
		#endregion
		#region Private methods
		private void RandomizeBoard()
		{
			string jsonContent = File.ReadAllText(Const.bingoPath);

			Dictionary<string, List<int>> bingoDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(jsonContent);

			Random random = new Random();
			List<string> usedEntries = new List<string>();

			foreach (Button button in this.buttonsList)
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
		#endregion
		#region Form Methods
		private void colorBtn_Click(object sender, EventArgs e)
		{
			if (colorDialog.ShowDialog() != DialogResult.Cancel)
			{
				colorBtn.ForeColor = colorDialog.Color;
				colorBtn.BackColor = colorDialog.Color;
			}
			this.hasSetColor = true;

			playerColor = Color.FromArgb(0, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);

			if (this.hasSetColor && this.hasSetName)
			{
				buttonsList.ForEach(button => { button.Enabled = true; });
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
				buttonsList.ForEach(button =>
				{
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

			Const.client.Send(buttonPressed.Text + ':' + playerColor + ":false");
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
		#endregion
		#region Utility methods
		private string ParseRGB(byte R, byte G, byte B)
		{
			return $"({R},{G},{B})";
		}
		#endregion
	}
}
