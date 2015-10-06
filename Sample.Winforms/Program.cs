using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sample.Common;
using Sample.Common.States;
using Sample.Winforms.ApplicationSwitcher;
using Sample.Winforms.ManagerApplication;
using Sample.Winforms.NewRequestEditor;
using Sample.Winforms.UserApplication;

namespace Sample.Winforms
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);


			var allRequests = new List<CreditRequest>();
			allRequests.Add(new CreditRequest
			{
				ID = Guid.NewGuid(),
				Amount = 2.50M,
				Justification = "I need food",
				CreatedOn = DateTime.Now,
				CreatedBy = "Andy Dote",
				State = typeof(AwaitingManagerApproval)
			});

			allRequests.Add(new CreditRequest
			{
				ID = Guid.NewGuid(),
				Amount = 2.50M,
				Justification = "I need food",
				CreatedOn = DateTime.Now,
				CreatedBy = "Andy Dote",
				State = typeof(Abandoned)
			});


			using (var userView = new UserView())
			using (var userPresenter = new UserPresenter(userView, allRequests))
			using (var managerView = new ManagerView())
			using (var managerPresenter = new ManagerPresenter(managerView, allRequests))
			using (var switcherView = new SwitcherView())
			using (var switcherPresenter = new SwitcherPresenter(switcherView, userPresenter.Display, managerPresenter.Display))
			{
				switcherPresenter.Display();
			}
		}
	}
}
