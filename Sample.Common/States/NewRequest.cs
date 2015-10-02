using Finite;

namespace Sample.Common.States
{
	public class NewRequest : State<ICreditSwitches>
	{
		public NewRequest()
		{
			LinkTo<AwaitingManagerApproval>(x => x.IsFilledOut);
			LinkTo<Abandoned>();
		}
	}
}
