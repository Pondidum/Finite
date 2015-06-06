namespace Finite.Tests.Acceptance.States
{
	public class LightOnFull : LightState
	{
		public LightOnFull()
		{
			LinkTo<LightOnDim>(l => l.OnBattery);
			LinkTo<LightOff>();
		}
	}
}