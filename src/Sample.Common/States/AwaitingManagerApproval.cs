using System.Security.Claims;
using Finite;

namespace Sample.Common.States
{
	public class AwaitingManagerApproval : State<CreditRequest>
	{
		public AwaitingManagerApproval()
		{
			LinkTo<Approved>(x => IsManager());
			LinkTo<Rejected>(x => IsManager());
		}

		private static bool IsManager()
		{
			return ClaimsPrincipal.Current.IsInRole("manager");
		}
	}
}
