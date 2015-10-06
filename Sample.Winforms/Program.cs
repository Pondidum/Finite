using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sample.Common;
using Sample.Common.States;
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

			ClaimsPrincipal.Current.AddIdentity(new GenericIdentity("Andy Dote"));


			var allRequests = new List<CreditRequest>();
			allRequests.Add(new CreditRequest
			{
				Amount = 2.50M,
				Justification = "I need food",
				CreatedOn = DateTime.Now,
				CreatedBy = "Andy Dote",
				State = typeof(AwaitingManagerApproval)
			});

			allRequests.Add(new CreditRequest
			{
				Amount = 2.50M,
				Justification = "I need food",
				CreatedOn = DateTime.Now,
				CreatedBy = "Andy Dote",
				State = typeof(Abandoned)
			});

			using (var view = new UserView())
			using (var presenter = new UserPresenter(view, allRequests))
			{
				presenter.Display();
			}
		}
	}
}
