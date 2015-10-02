using Finite;

namespace Sample.Common.States
{
	public class AwaitingManagerApproval : State<ICreditSwitches>
	{
		public AwaitingManagerApproval()
		{
			LinkTo<Approved>(x => x.IsManager);
			LinkTo<Rejected>(x => x.IsManager);
		}
	}
}
