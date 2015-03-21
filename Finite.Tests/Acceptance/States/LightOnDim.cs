namespace Finite.Tests.Acceptance.States
{
	public class LightOnDim : LightState
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