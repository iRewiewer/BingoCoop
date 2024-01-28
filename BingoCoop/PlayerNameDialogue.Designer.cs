namespace BingoCoop
{
	partial class PlayerNameDialogue
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			submitBtn = new Button();
			nameLb = new Label();
			nameTBox = new TextBox();
			SuspendLayout();
			// 
			// submitBtn
			// 
			submitBtn.Location = new Point(49, 62);
			submitBtn.Name = "submitBtn";
			submitBtn.Size = new Size(90, 29);
			submitBtn.TabIndex = 6;
			submitBtn.Text = "Submit";
			submitBtn.UseVisualStyleBackColor = true;
			submitBtn.Click += submitBtn_Click;
			// 
			// nameLb
			// 
			nameLb.AutoSize = true;
			nameLb.Location = new Point(12, 11);
			nameLb.Name = "nameLb";
			nameLb.Size = new Size(102, 17);
			nameLb.TabIndex = 5;
			nameLb.Text = "Choose a name:";
			// 
			// nameTBox
			// 
			nameTBox.Location = new Point(12, 31);
			nameTBox.MaxLength = 16;
			nameTBox.Name = "nameTBox";
			nameTBox.Size = new Size(174, 25);
			nameTBox.TabIndex = 4;
			// 
			// PlayerNameDialogue
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(208, 108);
			Controls.Add(submitBtn);
			Controls.Add(nameLb);
			Controls.Add(nameTBox);
			Name = "PlayerNameDialogue";
			Text = "UsernameDialogue";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button submitBtn;
		private Label nameLb;
		private TextBox nameTBox;
	}
}