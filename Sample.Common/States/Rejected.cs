using Finite;

namespace Sample.Common.States
{
	public class Rejected : State<CreditSwitches>
	{
		public Rejected()
		{
			LinkTo<AwaitingManagerApproval>();
		}
	}
}
