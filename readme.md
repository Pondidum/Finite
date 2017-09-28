# Finite

A simple finite state machine written in C#

## Installation

```bash
PM> Install-Package Finite
```

## Usage

Declare a state arg object which is used to control available state transitions:

```c#
public class StateArgs
{
	public boolean OnBattery { get; set; }
}
```

A class for each of the states to use in your state machine:

```c#
public class LightOff : State<StateArgs>
{
	public LightOff()
	{
		LinkTo<LightOnFull>(l => l.OnBattery == false);
		LinkTo<LightOnDim>(l => l.OnBattery);
	}
}

public class LightOnDim : State<StateArgs>
{
	public LightOnDim()
	{
		LinkTo<LightOnFull>(l => l.OnBattery == false);
		LinkTo<LightOff>();
	}
}

public class LightOnFull : State<StateArgs>
{
	public LightOnFull()
	{
		LinkTo<LightOnDim>(l => l.OnBattery);
		LinkTo<LightOff>();
	}
}
```

Configure the machine with the states:

```c#
var allStates = new State<LightsSwitches>[]
{
	new LightOff(),
	new LightOnDim(),
	new LightOnFull(),
};

var machine = new StateMachine<LightSwitches>(
	new ManualStateProvider<LightsSwitches>(allStates),
	new LightSwitches());
```

The state machine can then be used:

```c#
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
```

## State Discovery

There are a couple of ways of adding states to the state machine.  The first seen in the previous examples is to specify them by an array (or `IEnumerable<Type>`):

```c#
var allStates = new State<LightsSwitches>[]
{
	new LightOff(),
	new LightOnDim(),
	new LightOnFull(),
};

var machine = new StateMachine<LightSwitches>(
	new ManualStateProvider<LightsSwitches>(allStates),
	switches);
```

This method is fine if you have just few states to specify, but when you get into larger state machines, it can become unweildy.

The alternate built in way is to use the `ScanningStateProvider` class, which will look for all States in the assembly which can be used by your state machine (in this example, anything inheriting `State<LightSwitches>`):

```c#
var machine = new StateMachine<LightSwitches>(
	new ScanningStateProvider<LightSwitches>(),
	switches);
```

This StateProvider also supports custom state creation, by means of a lambda:

```c#
var stateProvider = new ScanningStateProvider<LightSwitches>(state => (State<LightSwitches>)_container.GetInstance(state));
```

To create your own StateProvider, you just need to implement `IStateProvider<TSwitches>`.  The source for both the `ManualStateProvider` and `ScanningStateProvider` are [viewable here][https://github.com/Pondidum/Finite/tree/master/Finite/StateProviders].

## State Transition Notifications

The state machine supports several notification events.  Each state can receive an `OnEnter` and `OnLeave` notification, and the state machine itself supports `OnEnter`, `OnLeave` and `OnReset`.

When `machine.TransitionTo<TState>()` is called, the `OnEnter` and `OnLeave` methods get called, in the following order:

1. Configuration.OnLeave
2. OldState.OnLeave
3. NewState.OnEnter
4. Configuration.OnEnter

When `machine.ResetTo<TState>()` is called, only the `Configuration.OnReset()` method is called.

You can do anything in the handlers you want, but in general the handlers on the individual states are used to trigger behaviour in your application, such as toggling display elements, and the handlers on the configuration are used for logging, auditing, etc of all state changes.

Each handler is passed a `StateChangeEventArgs` object, which contains properties for the switches, previous state and current state.
