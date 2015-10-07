# Sample.CustomStateLoading

This sample shows how you could go about implementing loading and transitioning states where the current state is stored as an enum value, rather than a `Type`.

This is a useful way to implement a new state machine on top of a legacy system which may already have some notion of state stored in a custom manner (either by name, or an enum in a database column etc.)

Instead of inheriting directly from `State<TSwitches>`, all the states inherit `CustomState<TSwitches>`, which provides an extra property for the state's enum value:

```csharp
public abstract class CustomState<T> : State<T>
{
	public abstract Progress Type { get; }
}
```

This is paired with an `IStateProvider<>` decorator class called `MappingStateProvider`:

```csharp
public class MappingStateProvider : IStateProvider<CreditRequest>
{
	private readonly Lazy<List<CustomState<CreditRequest>>> _states;

	public MappingStateProvider(IStateProvider<CreditRequest> other)
	{
		_states = new Lazy<List<CustomState<CreditRequest>>>(() => other
			.Execute()
			.Cast<CustomState<CreditRequest>>()
			.ToList());
	}

	public State<CreditRequest> StateFrom(Progress type)
	{
		return _states.Value.Single(state => state.Type == type);
	}

	public IEnumerable<State<CreditRequest>> Execute()
	{
		return _states.Value;
	}
}
```

This can be used in the state machine setup to allow you to reset the machine to a know state based on the enum value:

```csharp
var scanner = new ScanningStateProvider<CreditRequest>();
var provider = new MappingStateProvider(scanner);

var request = LoadFromDatabase();
var currentState = provider.StateFrom(request.Progress).GetType();

var machine = new StateMachine<CreditRequest>(provider, request);
machine.ResetTo(currentState);
```
