using Finite;

namespace Sample.Common.States
{
	public class NewRequest : State<CreditSwitches>
	{
		public NewRequest()
		{
			LinkTo<AwaitingManagerApproval>(x => x.IsFilledOut);
			LinkTo<Abandoned>();
		}
	}
}
