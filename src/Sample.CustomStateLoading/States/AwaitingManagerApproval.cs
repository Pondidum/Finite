using System.Security.Claims;
using Finite;

namespace Sample.CustomStateLoading.States
{
	public class AwaitingManagerApproval : CustomState<CreditRequest>
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

		public override Progress Type
		{
			get { return Progress.AwaitingManagerApproval; }
		}
	}
}
