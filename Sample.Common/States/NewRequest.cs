using Finite;

namespace Sample.Common.States
{
	public class NewRequest : State<CreditRequest>
	{
		public NewRequest()
		{
			LinkTo<NewRequest>();	//explicitly allow samestate transitioning
			LinkTo<AwaitingManagerApproval>(request => request.Justification.Trim() != "" && request.Amount > 0);
			LinkTo<Abandoned>();
		}
	}
}
