namespace Sample.Winforms.UserApplication
{
	partial class UserView
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
			this.btnNew = new System.Windows.Forms.Button();
			this.btnAbandon = new System.Windows.Forms.Button();
			this.lst = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// btnNew
			// 
			this.btnNew.Location = new System.Drawing.Point(12, 226);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(75, 23);
			this.btnNew.TabIndex = 0;
			this.btnNew.Text = "New";
			this.btnNew.UseVisualStyleBackColor = true;
			// 
			// btnAbandon
			// 
			this.btnAbandon.Location = new System.Drawing.Point(93, 226);
			this.btnAbandon.Name = "btnAbandon";
			this.btnAbandon.Size = new System.Drawing.Size(75, 23);
			this.btnAbandon.TabIndex = 0;
			this.btnAbandon.Text = "Abandon";
			this.btnAbandon.UseVisualStyleBackColor = true;
			// 
			// lst
			// 
			this.lst.FormattingEnabled = true;
			this.lst.IntegralHeight = false;
			this.lst.Location = new System.Drawing.Point(12, 12);
			this.lst.Name = "lst";
			this.lst.Size = new System.Drawing.Size(156, 208);
			this.lst.TabIndex = 1;
			// 
			// UserView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(185, 261);
			this.Controls.Add(this.lst);
			this.Controls.Add(this.btnAbandon);
			this.Controls.Add(this.btnNew);
			this.Name = "UserView";
			this.Text = "UserView";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.Button btnAbandon;
		private System.Windows.Forms.ListBox lst;
	}
}