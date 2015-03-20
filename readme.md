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

	var allStates = new[]
	{
		typeof(LightOff),
		typeof(LightOnDim),
		typeof(LightOnFull)
	};

	var machine = new StateMachine<TestArgs>(states => states.Are(allStates), new TestArgs());

The state machine can then be used:

	//reset the state machine to an initial state:
	machine.ResetTo<LightOff>();
	machine.CurrentState.ShouldBeOfType<LightOff>();

	//all of the possible transitions
	machine.AllTargetStates
		.Select(state => state.GetType())
		.ShouldBe(new[] { typeof(LightOnDim), typeof(LightOnFull) });

	//all of the states which can currently be transitioned to
	machine.ActiveTargetStates
		.Select(state => state.GetType())
		.ShouldBe(new[] { typeof(LightOnFull) });

	//all of the states which can't be currently transitioned to
	machine.InactiveTargetStates
		.Select(state => state.GetType())
		.ShouldBe(new[] { typeof(LightOnDim) });

	//LightOnDim is not a valid transition at the moment:
	Should.Throw<InvalidTransitionException>(() => machine.TransitionTo<LightOnDim>());
	machine.CurrentState.ShouldBeOfType<LightOff>();

	//LightOnFull is valid:
	machine.TransitionTo<LightOnFull>();
	machine.CurrentState.ShouldBeOfType<LightOnFull>();
