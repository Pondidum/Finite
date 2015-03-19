namespace Finite.Tests.Acceptance.States
{
	public class LightOff : State<LightsSwitches>
	{
		public LightOff()
		{
			Configure(state =>
			{
				state.LinkTo<LightOnFull>().When(l => l.OnBattery == false);
				state.LinkTo<LightOnDim>().When(l => l.OnBattery);
			});
		}
	}
}
