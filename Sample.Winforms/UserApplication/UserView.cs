using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Sample.Common;
using Sample.Common.States;

namespace Sample.Winforms.UserApplication
{
	public partial class UserView : Form, IUserView
	{
		public UserView()
		{
			InitializeComponent();
		}

		public event EventAction CreditRequestSelected;
		public event EventAction CreateNew;
		public event EventAction Abandon;

		public IEnumerable<CreditRequest> CreditRequests
		{
			set
			{
				var selected = lst.SelectedItem;
				lst.Items.Clear();
				lst.Items.AddRange(value.Select(cr => new CreditRequestViewModel(cr)).Cast<object>().ToArray());
				lst.SelectedItem = selected;
			}
		}

		public CreditRequest SelectedRequest
		{
			get { return (lst.SelectedItem as CreditRequestViewModel)?.Request; }
			set { lst.SelectedItem = lst.Items.Cast<CreditRequestViewModel>().FirstOrDefault(vm => vm.Request == value); }
		}

		public bool AbandonEnabled
		{
			set { btnAbandon.Enabled = value; }
		}


		private void btnNew_Click(object sender, System.EventArgs e)
		{
			CreateNew();
		}

		private void btnAbandon_Click(object sender, System.EventArgs e)
		{
			Abandon();
		}

		private void lst_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			CreditRequestSelected();
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
				var maxLength = 15;
				var shortText = Request.Justification.Length > maxLength
					? Request.Justification.Substring(0, maxLength - 1) + "..."
					: Request.Justification;

                return $"£{Request.Amount} [{Request.State.Name}]:{shortText}";
			}
		}
	}
}
