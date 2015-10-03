using Finite;

namespace Sample.Common.States
{
	public class Rejected : State<CreditRequest>
	{
		public Rejected()
		{
			LinkTo<AwaitingManagerApproval>();
		}
	}
}
