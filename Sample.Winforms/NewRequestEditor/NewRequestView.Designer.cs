namespace Sample.Winforms.NewRequestEditor
{
	partial class NewRequestView
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
			this.btnSubmit = new System.Windows.Forms.Button();
			this.txtJustification = new System.Windows.Forms.TextBox();
			this.numAmount = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numAmount)).BeginInit();
			this.SuspendLayout();
			// 
			// btnSubmit
			// 
			this.btnSubmit.Location = new System.Drawing.Point(193, 238);
			this.btnSubmit.Name = "btnSubmit";
			this.btnSubmit.Size = new System.Drawing.Size(75, 23);
			this.btnSubmit.TabIndex = 4;
			this.btnSubmit.Text = "Submit";
			this.btnSubmit.UseVisualStyleBackColor = true;
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			// 
			// txtJustification
			// 
			this.txtJustification.Location = new System.Drawing.Point(123, 80);
			this.txtJustification.Multiline = true;
			this.txtJustification.Name = "txtJustification";
			this.txtJustification.Size = new System.Drawing.Size(307, 152);
			this.txtJustification.TabIndex = 3;
			// 
			// numAmount
			// 
			this.numAmount.DecimalPlaces = 2;
			this.numAmount.Location = new System.Drawing.Point(123, 54);
			this.numAmount.Name = "numAmount";
			this.numAmount.Size = new System.Drawing.Size(120, 20);
			this.numAmount.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(28, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Amount Required";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(55, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(62, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Justification";
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(274, 238);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 4;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(355, 238);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// NewRequestView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(450, 273);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.numAmount);
			this.Controls.Add(this.txtJustification);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnSubmit);
			this.Name = "NewRequestView";
			this.Text = "NewRequestView";
			((System.ComponentModel.ISupportInitialize)(this.numAmount)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnSubmit;
		private System.Windows.Forms.TextBox txtJustification;
		private System.Windows.Forms.NumericUpDown numAmount;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
	}
}