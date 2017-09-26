namespace Finite.Tests.Acceptance.States
{
	public class LightOnDim : LightState
	{
		public LightOnDim()
		{
			LinkTo<LightOnFull>(l => l.OnBattery == false);
			LinkTo<LightOff>();
		}
	}
}