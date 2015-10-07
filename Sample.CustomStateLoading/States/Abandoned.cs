using Finite;

namespace Sample.CustomStateLoading.States
{
	public class Abandoned : CustomState<CreditRequest>
	{
		public override Progress Type
		{
			get { return Progress.Abandoned; }
		}
	}
}
