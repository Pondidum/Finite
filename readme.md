Finite 
======

A simpleish finite state machine written in C#

Usage
-----

Declare a state arg object, this is what is queried for what transitions are available:

	public class StateArgs
	{
		public boolean OnBattery { get; set; }
	}

A couple of states for your machine:

	public class LightOff : State<StateArgs>
	{
		public LightOff()
		{
			LinkTo<LightOnFull>().When(l => l.OnBattery == false);
			LinkTo<LightOnDim>().When(l => l.OnBattery);
		}
	}

	public class LightOnDim : State<StateArgs>
	{
		public LightOnDim()
		{
			LinkTo<LightOnFull>().When(l => l.OnBattery == false);
			LinkTo<LightOff>();
		}
	}

	public class LightOnFull : State<StateArgs>
	{
		public LightOnFull()
		{
			LinkTo<LightOnDim>().When(l => l.OnBattery);
			LinkTo<LightOff>();
		}
	}
