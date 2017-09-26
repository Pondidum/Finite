using Finite;

namespace Sample.CustomStateLoading.States
{
	public class NewRequest : CustomState<CreditRequest>
	{
		public NewRequest()
		{
			LinkTo<NewRequest>();	//explicitly allow samestate transitioning
			LinkTo<AwaitingManagerApproval>(request => request.Justification.Trim() != "" && request.Amount > 0);
			LinkTo<Abandoned>();
		}

		public override Progress Type
		{
			get { return Progress.NewRequest; }
		}
	}
}
