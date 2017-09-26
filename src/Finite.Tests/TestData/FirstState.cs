namespace Finite.Tests.TestData
{
	public class FirstState : State<TestArgs>
	{
		public FirstState()
		{
			LinkTo<SecondState>();
		}
	}
}
