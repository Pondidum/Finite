using Finite;

namespace Sample.CustomStateLoading.States
{
	public class Rejected : CustomState<CreditRequest>
	{
		public Rejected()
		{
			LinkTo<NewRequest>();
		}

		public override Progress Type
		{
			get { return Progress.Rejected; }
		}
	}
}
