using System.Collections.Generic;
using System.Windows.Forms;
using Sample.Common;

namespace Sample.Winforms.UserApplication
{
	public interface IUserView
	{
		event EventAction CreditRequestSelected;
		event EventAction CreateNew;
		event EventAction Abandon;

		IEnumerable<CreditRequest> CreditRequests { set; }

		CreditRequest SelectedRequest { get; set; }
		bool AbandonEnabled { set; }

		DialogResult ShowDialog();
		void Close();
	}
}
