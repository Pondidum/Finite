using System.Windows.Forms;

namespace Sample.Winforms.ApplicationSwitcher
{
	public partial class SwitcherView : Form, ISwitcherView
	{
		public SwitcherView()
		{
			InitializeComponent();
		}

		public event EventAction UserClicked;
		public event EventAction ManagerClicked;

		private void btnUser_Click(object sender, System.EventArgs e)
		{
			UserClicked();
		}

		private void btnManager_Click(object sender, System.EventArgs e)
		{
			ManagerClicked();
		}
	}
}
