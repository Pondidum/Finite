using System.Windows.Forms;

namespace Sample.Winforms.NewRequestEditor
{
	public partial class NewRequestView : Form, INewRequestView
	{
		public NewRequestView()
		{
			InitializeComponent();
		}

		public event EventAction SaveRequest;
		public event EventAction SubmitRequest;
		public event EventAction CancelRequest;

		public decimal Amount
		{
			get { return numAmount.Value; }
			set { numAmount.Value = value; }
		}

		public string Justification
		{
			get { return txtJustification.Text.Trim(); }
			set { txtJustification.Text = value.Trim(); }
		}

		public bool SaveEnabled
		{
			get { return btnSave.Enabled; }
			set { btnSave.Enabled = value; }
		}

		public bool SubmitEnabled
		{
			get { return btnSubmit.Enabled; }
			set { btnSubmit.Enabled = value; }
		}

		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			SubmitRequest();
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			SaveRequest();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			CancelRequest();
		}
	}
}
