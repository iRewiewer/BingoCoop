using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BingoCoop
{
	public partial class PlayerNameDialogue : Form
	{
		public string playerName;

		public PlayerNameDialogue()
		{
			InitializeComponent();
		}

		private void submitBtn_Click(object sender, EventArgs e)
		{
			this.playerName = nameTBox.Text;
			// request connected names from server
			// check if unique

			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
