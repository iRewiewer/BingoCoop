namespace BingoCoop
{
	partial class Dialogue
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
			ipLb = new Label();
			ipTBox = new TextBox();
			portTBox = new TextBox();
			portLb = new Label();
			SuspendLayout();
			// 
			// submitBtn
			// 
			submitBtn.Anchor = AnchorStyles.None;
			submitBtn.Location = new Point(88, 84);
			submitBtn.Name = "submitBtn";
			submitBtn.Size = new Size(90, 29);
			submitBtn.TabIndex = 0;
			submitBtn.Text = "Submit";
			submitBtn.UseVisualStyleBackColor = true;
			submitBtn.Click += submitBtn_Click;
			// 
			// ipLb
			// 
			ipLb.Anchor = AnchorStyles.None;
			ipLb.AutoSize = true;
			ipLb.Location = new Point(18, 21);
			ipLb.Name = "ipLb";
			ipLb.Size = new Size(73, 17);
			ipLb.TabIndex = 1;
			ipLb.Text = "IP Address:";
			// 
			// ipTBox
			// 
			ipTBox.Anchor = AnchorStyles.None;
			ipTBox.Location = new Point(18, 41);
			ipTBox.MaxLength = 15;
			ipTBox.Name = "ipTBox";
			ipTBox.Size = new Size(109, 25);
			ipTBox.TabIndex = 2;
			// 
			// portTBox
			// 
			portTBox.Anchor = AnchorStyles.None;
			portTBox.Location = new Point(143, 41);
			portTBox.MaxLength = 5;
			portTBox.Name = "portTBox";
			portTBox.Size = new Size(100, 25);
			portTBox.TabIndex = 4;
			// 
			// portLb
			// 
			portLb.Anchor = AnchorStyles.None;
			portLb.AutoSize = true;
			portLb.Location = new Point(143, 21);
			portLb.Name = "portLb";
			portLb.Size = new Size(35, 17);
			portLb.TabIndex = 3;
			portLb.Text = "Port:";
			// 
			// Dialogue
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(269, 128);
			Controls.Add(portTBox);
			Controls.Add(portLb);
			Controls.Add(ipTBox);
			Controls.Add(ipLb);
			Controls.Add(submitBtn);
			Name = "Dialogue";
			Text = "Dialogue";
			FormClosing += OnFormClosing;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button submitBtn;
		private Label ipLb;
		private TextBox ipTBox;
		private TextBox portTBox;
		private Label portLb;
	}
}