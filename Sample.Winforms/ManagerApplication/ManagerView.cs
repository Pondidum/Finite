using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sample.Common;

namespace Sample.Winforms.ManagerApplication
{
	public partial class ManagerView : Form, IManagerView
	{
		public ManagerView()
		{
			InitializeComponent();
		}

		public event EventAction ApproveClicked;
		public event EventAction RejectClicked;
		public event EventAction CreditRequestSelected;

		public IEnumerable<CreditRequest> CreditRequests
		{
			set
			{
				lst.Items.Clear();
				lst.Items.AddRange(value.Select(cr => new CreditRequestViewModel(cr)).Cast<object>().ToArray());
			}
		}

		public CreditRequest SelectedRequest
		{
			get { return (lst.SelectedItem as CreditRequestViewModel)?.Request; }
			set { lst.SelectedItem = lst.Items.Cast<CreditRequestViewModel>().FirstOrDefault(vm => vm.Request == value); }
		}

		public void DisplayRequest(CreditRequest request)
		{
			lblTitle.Text = $"User {request.CreatedBy} requests £{request.Amount}";
			txtJustification.Text = request.Justification;
		}

		public void ClearDisplay()
		{
			lblTitle.Text = string.Empty;
			txtJustification.Text = string.Empty;
		}

		private void lst_SelectedIndexChanged(object sender, EventArgs e)
		{
			CreditRequestSelected();
		}

		private void btnAccept_Click(object sender, EventArgs e)
		{
			ApproveClicked();
		}

		private void btnReject_Click(object sender, EventArgs e)
		{
			RejectClicked();
		}

		private class CreditRequestViewModel
		{
			public CreditRequestViewModel(CreditRequest request)
			{
				Request = request;
			}

			public CreditRequest Request { get; }

			public override string ToString()
			{
				return $"{Request.CreatedBy} - £{Request.Amount}";
			}
		}

	}
}
