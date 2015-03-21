namespace Finite.Tests.Acceptance.States
{
	public class LightOnFull : LightState
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