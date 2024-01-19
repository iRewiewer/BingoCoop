namespace BingoCoop
{
	partial class Menu
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			joinBtn = new Button();
			hostBtn = new Button();
			SuspendLayout();
			// 
			// joinBtn
			// 
			joinBtn.Anchor = AnchorStyles.None;
			joinBtn.AutoSize = true;
			joinBtn.Location = new Point(86, 33);
			joinBtn.Name = "joinBtn";
			joinBtn.Size = new Size(92, 30);
			joinBtn.TabIndex = 0;
			joinBtn.Text = "Join";
			joinBtn.UseVisualStyleBackColor = true;
			joinBtn.Click += joinBtn_Click;
			// 
			// hostBtn
			// 
			hostBtn.Anchor = AnchorStyles.None;
			hostBtn.AutoSize = true;
			hostBtn.Location = new Point(86, 80);
			hostBtn.Name = "hostBtn";
			hostBtn.Size = new Size(92, 30);
			hostBtn.TabIndex = 1;
			hostBtn.Text = "Host";
			hostBtn.UseVisualStyleBackColor = true;
			hostBtn.Click += hostBtn_Click;
			// 
			// Menu
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(279, 147);
			Controls.Add(hostBtn);
			Controls.Add(joinBtn);
			Name = "Menu";
			Text = "Menu";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button joinBtn;
		private Button hostBtn;
	}
}