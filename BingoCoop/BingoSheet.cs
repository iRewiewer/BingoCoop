using Newtonsoft.Json;
using SuperSimpleTcp;

namespace BingoCoop
{
	public partial class BingoSheet : Form
	{
		private Click playerClick;
		private Form parentForm;

		private bool hasSetName = false;
		private bool hasSetColor = false;
		private bool finishedPlayerSetup = false;

		private List<Button> buttonsList = new List<Button>();
		private List<string> buttonsContentList = new List<string>();

		public BingoSheet(Form parentForm)
		{
			this.parentForm = parentForm;
			this.playerClick = new Click();

			InitializeComponent();

			if(Const.isHosting)
			{
				List<string> content = GenerateBoardContent();
				InitializeBoard(content);
			}

			if(Const.hasRaisedExitError)
			{
				Const.rootForm.Show();
				this.parentForm.Close();
				this.Close();
			}
		}
		#region Public methods
		public void UpdateBoardOnClick(string data)
		{
			Log.Message($"Update board on click with data: {data}");
			Click click = new Click();

			try
			{
				click = JsonConvert.DeserializeObject<Click>(data);
			}
			catch (Exception e)
			{
				Log.Error("Could not parse click data.", Log.ErrorType.ShouldNotHappen);
			}

			this.Invoke((MethodInvoker)(() =>
			{
				foreach (Button button in buttonsList)
				{
					if (button.Text == click.ButtonText)
					{
						button.BackColor = click.PlayerColor;
						break;
					}
				}
			}
			));
		}
		#endregion
		#region Private methods
		public void InitializeBoard(List<string> content)
		{
			if(content == null || Const.hasRaisedExitError)
			{
				return;
			}

			// Initialize board with the random values
			Const.hasReceivedBoard = true;
			for (int i = 1; i <= 25; i++)
			{
				Button currentButton = (Button)this.Controls.Find($"Button{i}", true).FirstOrDefault();
				currentButton.Text = content[i - 1];
				buttonsList.Add(currentButton);
				buttonsContentList.Add(currentButton.Text);
			}

			Const.buttonsContentJSON = JsonConvert.SerializeObject(buttonsContentList);
		}
		private List<string> GenerateBoardContent()
		{
			List<string> buttonsContent = new List<string>();
			string jsonContent = string.Empty;
			Dictionary<string, List<int>>? bingoDictionary = new Dictionary<string, List<int>>();

			try
			{
				jsonContent = File.ReadAllText(Const.bingoPath);
			}
			catch (Exception e)
			{
				string errorMessage = $"Could not read bingo file from path:\n'{Const.bingoPath}'.\nProgram will exit.";
				Log.Error(errorMessage, Log.ErrorType.FileHandling);
				MessageBox.Show(errorMessage);
				Const.hasRaisedExitError = true;
				return null;
			}

			try
			{
				bingoDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(jsonContent);
			}
			catch (Exception e)
			{
				string errorMessage = $"Could not parse bingo file, possibly wrongly formatted.\nProgram will exit.";
				Log.Error(errorMessage, Log.ErrorType.FileHandling);
				MessageBox.Show(errorMessage);
				Const.hasRaisedExitError = true;
				return null;
			}
			
			Random random = new Random();
			List<string> usedEntries = new List<string>();

			for (int i = 1; i <= 25; i++)
			{
				KeyValuePair<string, List<int>> randomEntry;
				do
				{
					randomEntry = bingoDictionary.ElementAt(random.Next(bingoDictionary.Count));
				} while (usedEntries.Contains(randomEntry.Key));

				string buttonText = randomEntry.Key;

				if (randomEntry.Value != null)
				{
					int randomValue = randomEntry.Value[random.Next(randomEntry.Value.Count)];
					buttonText = buttonText.Replace("_", randomValue.ToString());
				}

				buttonText = buttonText.Replace("{", "").Replace("}", "").Replace("|", "");
				buttonsContent.Add(buttonText);
				usedEntries.Add(randomEntry.Key);
			}

			return buttonsContent;
		}
		#endregion
		#region Form Methods
		private void ColorBtn_Click(object sender, EventArgs e)
		{
			if (colorDialog.ShowDialog() != DialogResult.Cancel)
			{
				colorBtn.ForeColor = colorDialog.Color;
				colorBtn.BackColor = colorDialog.Color;
			}
			this.hasSetColor = true;

			playerClick.PlayerColor = Color.FromArgb(255, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);

			if (this.hasSetColor && this.hasSetName && !this.finishedPlayerSetup)
			{
				buttonsList.ForEach(button =>
				{
					button.Enabled = true;
					button.BackColor = Color.White;
				});

				finishedPlayerSetup = true;
			}
		}
		private void PlayerNameBtn_Click(object sender, EventArgs e)
		{
			PlayerNameDialogue playerNameDialogue = new PlayerNameDialogue();
			if (playerNameDialogue.ShowDialog() == DialogResult.OK)
			{
				playerNameBtn.Text = playerNameDialogue.playerName;
				playerClick.PlayerName = playerNameBtn.Text;
			};
			this.hasSetName = true;

			if (this.hasSetColor && this.hasSetName && !this.finishedPlayerSetup)
			{
				buttonsList.ForEach(button =>
				{
					button.Enabled = true;
					button.BackColor = Color.White;
				});

				finishedPlayerSetup = true;
			}
		}
		private void BingoButton_Click(object sender, EventArgs e)
		{
			Button buttonPressed = (Button)sender;
			Color backup = playerClick.PlayerColor;

			if (buttonPressed.BackColor != Color.White && buttonPressed.BackColor != playerClick.PlayerColor)
			{
				Log.Message("Tried clicking on another player's tile.");
				return;
			}

			if(buttonPressed.BackColor == playerClick.PlayerColor)
			{
				Log.Message("Deselecting tile.");
				playerClick.PlayerColor = Color.White;
			}

			playerClick.ButtonText = buttonPressed.Text;
			string clickJson = JsonConvert.SerializeObject(playerClick, Formatting.Indented);
			Const.client.Send(clickJson);

			// reset color, just in case
			playerClick.PlayerColor = backup;
		}
		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			Log.Message("Disconnecting from server.");

			Const.client.Disconnect();
			if (Const.isHosting)
			{
				Log.Message("Shutting down server.");
				Const.server.Stop();
			}

			Const.rootForm.Show();
			this.parentForm.Close();
		}
		#endregion
	}
}