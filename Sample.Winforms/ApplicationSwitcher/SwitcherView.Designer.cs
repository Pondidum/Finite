namespace Sample.Winforms.ApplicationSwitcher
{
	partial class SwitcherView
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
			this.btnUser = new System.Windows.Forms.Button();
			this.btnManager = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnUser
			// 
			this.btnUser.Location = new System.Drawing.Point(12, 12);
			this.btnUser.Name = "btnUser";
			this.btnUser.Size = new System.Drawing.Size(75, 23);
			this.btnUser.TabIndex = 0;
			this.btnUser.Text = "User";
			this.btnUser.UseVisualStyleBackColor = true;
			this.btnUser.Click += new System.EventHandler(this.btnUser_Click);
			// 
			// btnManager
			// 
			this.btnManager.Location = new System.Drawing.Point(93, 12);
			this.btnManager.Name = "btnManager";
			this.btnManager.Size = new System.Drawing.Size(75, 23);
			this.btnManager.TabIndex = 1;
			this.btnManager.Text = "Manager";
			this.btnManager.UseVisualStyleBackColor = true;
			this.btnManager.Click += new System.EventHandler(this.btnManager_Click);
			// 
			// SwitcherView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(180, 47);
			this.Controls.Add(this.btnManager);
			this.Controls.Add(this.btnUser);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SwitcherView";
			this.Text = "SwitcherView";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnUser;
		private System.Windows.Forms.Button btnManager;
	}
}