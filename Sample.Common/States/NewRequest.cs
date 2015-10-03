using Finite;

namespace Sample.Common.States
{
	public class NewRequest : State<CreditRequest>
	{
		public NewRequest()
		{
			LinkTo<AwaitingManagerApproval>(request => request.Justification.Trim() != "" && request.Amount > 0);
			LinkTo<Abandoned>();
		}
	}
}
