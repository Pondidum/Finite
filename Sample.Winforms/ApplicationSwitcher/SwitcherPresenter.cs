using System;

namespace Sample.Winforms.ApplicationSwitcher
{
	public class SwitcherPresenter : IDisposable
	{
		private readonly ISwitcherView _view;
		private readonly Action _displayUser;
		private readonly Action _displayManager;

		public SwitcherPresenter(ISwitcherView view, Action displayUser, Action displayManager)
		{
			_view = view;
			_displayUser = displayUser;
			_displayManager = displayManager;

			_view.UserClicked += OnUserClicked;
			_view.ManagerClicked += OnManagerClicked;
		}

		public void Display()
		{
			_view.ShowDialog();
		}

		private void OnUserClicked()
		{
			_displayUser();
		}

		private void OnManagerClicked()
		{
			_displayManager();
		}

		public void Dispose()
		{
			_view.UserClicked -= OnUserClicked;
			_view.ManagerClicked -= OnManagerClicked;
		}
	}
}
