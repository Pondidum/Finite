using Finite;

namespace Sample.Common.States
{
	public class Approved : State<CreditSwitches>
	{
		public Approved()
		{
			LinkTo<Abandoned>();
		}
	}
}
