using Finite;

namespace Sample.Common.States
{
	public class Approved : State<CreditRequest>
	{
		public Approved()
		{
			LinkTo<Abandoned>();
		}
	}
}
