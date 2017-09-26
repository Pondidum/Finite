using Finite;

namespace Sample.CustomStateLoading.States
{
	public class Approved : CustomState<CreditRequest>
	{
		public Approved()
		{
			LinkTo<Abandoned>();
		}

		public override Progress Type
		{
			get { return Progress.Approved; }
		}
	}
}
