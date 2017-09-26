namespace Finite.Tests.Acceptance.States
{
	public class LightOff : LightState
	{
		public LightOff()
		{
			LinkTo<LightOnFull>(l => l.OnBattery == false);
			LinkTo<LightOnDim>(l => l.OnBattery);
		}
	}
}
