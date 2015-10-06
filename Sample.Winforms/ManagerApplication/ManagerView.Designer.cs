namespace Sample.Winforms.ManagerApplication
{
	partial class ManagerView
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
			this.lst = new System.Windows.Forms.ListBox();
			this.lblTitle = new System.Windows.Forms.Label();
			this.txtJustification = new System.Windows.Forms.TextBox();
			this.btnReject = new System.Windows.Forms.Button();
			this.btnAccept = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lst
			// 
			this.lst.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lst.FormattingEnabled = true;
			this.lst.IntegralHeight = false;
			this.lst.Location = new System.Drawing.Point(12, 12);
			this.lst.Name = "lst";
			this.lst.Size = new System.Drawing.Size(146, 330);
			this.lst.TabIndex = 0;
			this.lst.SelectedIndexChanged += new System.EventHandler(this.lst_SelectedIndexChanged);
			// 
			// lblTitle
			// 
			this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitle.Location = new System.Drawing.Point(164, 12);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(433, 34);
			this.lblTitle.TabIndex = 1;
			this.lblTitle.Text = "User {name} requests £{amount}";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtJustification
			// 
			this.txtJustification.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtJustification.Location = new System.Drawing.Point(164, 49);
			this.txtJustification.Multiline = true;
			this.txtJustification.Name = "txtJustification";
			this.txtJustification.ReadOnly = true;
			this.txtJustification.Size = new System.Drawing.Size(433, 293);
			this.txtJustification.TabIndex = 2;
			// 
			// btnReject
			// 
			this.btnReject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnReject.Location = new System.Drawing.Point(522, 348);
			this.btnReject.Name = "btnReject";
			this.btnReject.Size = new System.Drawing.Size(75, 23);
			this.btnReject.TabIndex = 3;
			this.btnReject.Text = "Reject";
			this.btnReject.UseVisualStyleBackColor = true;
			this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
			// 
			// btnAccept
			// 
			this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAccept.Location = new System.Drawing.Point(441, 348);
			this.btnAccept.Name = "btnAccept";
			this.btnAccept.Size = new System.Drawing.Size(75, 23);
			this.btnAccept.TabIndex = 4;
			this.btnAccept.Text = "Accept";
			this.btnAccept.UseVisualStyleBackColor = true;
			this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
			// 
			// ManagerView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(609, 383);
			this.Controls.Add(this.btnAccept);
			this.Controls.Add(this.btnReject);
			this.Controls.Add(this.txtJustification);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.lst);
			this.Name = "ManagerView";
			this.Text = "ManagerView";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox lst;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.TextBox txtJustification;
		private System.Windows.Forms.Button btnReject;
		private System.Windows.Forms.Button btnAccept;
	}
}