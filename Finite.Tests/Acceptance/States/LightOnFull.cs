namespace Finite.Tests.Acceptance.States
{
	public class LightOnFull : State<LightsSwitches>
	{
		public LightOnFull()
		{
			Configure(state =>
			{
				state.LinkTo<LightOnDim>().When(l => l.OnBattery);
				state.LinkTo<LightOff>();
			});
		}
	}
}