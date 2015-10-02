using Finite;

namespace Sample.Common.States
{
	public class Rejected : State<ICreditSwitches>
	{
		public Rejected()
		{
			LinkTo<AwaitingManagerApproval>();
		}
	}
}
