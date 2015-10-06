using System.Collections.Generic;
using System.Windows.Forms;
using Sample.Common;

namespace Sample.Winforms.ManagerApplication
{
	public interface IManagerView
	{
		event EventAction ApproveClicked;
		event EventAction RejectClicked;
		event EventAction CreditRequestSelected;

		IEnumerable<CreditRequest> CreditRequests { set; }
		CreditRequest SelectedRequest { get; set; }

		void DisplayRequest(CreditRequest request);
		void ClearDisplay();

		DialogResult ShowDialog();
		void Close();
	}
}
