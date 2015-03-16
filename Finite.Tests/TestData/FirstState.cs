namespace Finite.Tests.TestData
{
	public class FirstState : State<TestArgs>
	{
		public FirstState()
		{
			Configure(state =>
			{
				state.LinkTo<SecondState>().When(arg => true);
			});
		}
	}
}
