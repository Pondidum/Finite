using System.Windows.Forms;

namespace Sample.Winforms.ApplicationSwitcher
{
	public interface ISwitcherView
	{
		event EventAction UserClicked;
		event EventAction ManagerClicked;

		DialogResult ShowDialog();
		void Close();
	}
}
