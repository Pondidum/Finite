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

Configure the machine with the states:

	var args = new StateArgs();
	var machine = new Finite.StateMachine<StateArgs>();

	machine.InitialiseFromThis();
	machine.BindTo(args);
	machine.SetStateTo<LightOff>();

The key is `InitialiseFromThis()`.  This looks through the current assembly for all types which inherits from `State<StateArgs>`, and loads them into the state machine.

The state machine can then be used:
	
	// prints LightOnFull, LightOnDim
	foreach (var item in machine.GetAllTargetStates())
		Console.Writeline(item);

	// prints LightOnFull, LightOnDim
	args.OnBattery = false;
	foreach (var item in machine.GetActiveTargetStates())
		Console.Writeline(item);

	// prints LightOnDim
	args.OnBattery = true;
	foreach (var item in machine.GetActiveTargetStates())
		Console.Writeline(item);

