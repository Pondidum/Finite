namespace Finite.Tests.Acceptance.States
{
	public class LightOnDim : State<LightsSwitches>
	{
		public LightOnDim()
		{
			Configure(state =>
			{
				state.LinkTo<LightOnFull>().When(l => l.OnBattery == false);
				state.LinkTo<LightOff>();
			});
		}
	}
}