using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Windows.Forms;

namespace Sample.Winforms.ApplicationSwitcher
{
	public class SwitcherPresenter : IDisposable
	{
		private readonly ISwitcherView _view;
		private readonly Action _displayUser;
		private readonly Action _displayManager;

		private readonly GenericIdentity _identity;

		public SwitcherPresenter(ISwitcherView view, Action displayUser, Action displayManager)
		{
			_view = view;
			_displayUser = displayUser;
			_displayManager = displayManager;

			_view.UserClicked += OnUserClicked;
			_view.ManagerClicked += OnManagerClicked;

			_identity = new GenericIdentity("Andy Dote");

			ClaimsPrincipal.Current.AddIdentity(_identity);
		}

		public void Display()
		{
			_view.ShowDialog();
		}

		private void OnUserClicked()
		{
			var claim = GetClaim();

			if (claim != null)
				_identity.RemoveClaim(claim);

			_displayUser();
		}

		private void OnManagerClicked()
		{
			var claim = GetClaim();

			if (claim == null)
				_identity.AddClaim(new Claim(ClaimTypes.Role, "manager"));

			_displayManager();
		}

		private Claim GetClaim()
		{
			return _identity
				.Claims
				.FirstOrDefault(c => c.Type == ClaimTypes.Role && c.Value == "manager");
		}

		public void Dispose()
		{
			_view.UserClicked -= OnUserClicked;
			_view.ManagerClicked -= OnManagerClicked;
		}
	}
}
