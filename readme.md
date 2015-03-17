#Finite

A simple finite state machine written in C#

##Usage

Declare a state arg object which is used to control available state transitions:

	public class StateArgs
	{
		public boolean OnBattery { get; set; }
	}

A class for each of the states to use in your state machine:

	public class LightOff : State<StateArgs>
	{
		public LightOff()
		{
			Configure(state => {
				state.LinkTo<LightOnFull>().When(l => l.OnBattery == false);
				state.LinkTo<LightOnDim>().When(l => l.OnBattery);
			});
		}
	}

	public class LightOnDim : State<StateArgs>
	{
		public LightOnDim()
		{
			Configure(state => {
				state.LinkTo<LightOnFull>().When(l => l.OnBattery == false);
				state.LinkTo<LightOff>();
			});
		}
	}

	public class LightOnFull : State<StateArgs>
	{
		public LightOnFull()
		{
			Configure(state => {
				state.LinkTo<LightOnDim>().When(l => l.OnBattery);
				state.LinkTo<LightOff>();
			});
		}
	}

Configure the machine with the states:

	var states = new ManualStateProvider<TestArgs>(new Type[]
	{
		typeof(LightOff),
		typeof(LightOnDim),
		typeof(LightOnFull)
	});

	var machine = new StateMachine<TestArgs>(states, new TestArgs());

The state machine can then be used:

	machine.SetStateTo<LightOff>();

	machine.CurrentState.ShouldBeOfType<LightOff>();

	machine.GetAllTargetStates().ShouldBe()
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

