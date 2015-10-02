using Finite;

namespace Sample.Common.States
{
	public class Approved : State<ICreditSwitches>
	{
		public Approved()
		{
			LinkTo<Abandoned>();
		}
	}
}
