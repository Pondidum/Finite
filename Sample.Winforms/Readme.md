
# Sample.Winforms

This sample shows how you might use the FSM from a desktop application.

The first screen displayed (`ApplicationSwitcherView`) simulates the different environments a user and their manager would be using.

When you run the `UserApplication`, the thread's `ClaimsPrincipal.Current` identity has no extra roles added, and thus the checks in [AwaitingManagerApproval][state-awaiting] for being allow to transition to `Approved` or `Rejected` will not pass.

When you run the `ManagerApplication`, the thread's `ClaimsPrincipal.Current` has a new Claim added: `new Claim(ClaimTypes.Role, "manager")` allowing transition from `AwaitingManagerApproval` to `Approved` and `Rejected`.

The state machine is created using a builder class, which ensures that the `CreditRequest` has it's `State` property updated whenever the state machine transitions to a new state, or is reset.

```csharp
public class StateMachineBuilder
{
	public static StateMachine<CreditRequest> Create(CreditRequest request)
	{
		var provider = new ScanningStateProvider<CreditRequest>();

		var config = new MachineConfiguration<CreditRequest>();
		config.OnStateChange(
			reset: (sender, args) => args.Switches.State = args.Next.GetType(),
			enter: (sender, args) => args.Switches.State = args.Next.GetType()
		);

		var machine = new StateMachine<CreditRequest>(config, provider, request);
		machine.ResetTo(request.State);

		return machine;
	}
}
```


[state-awaiting]: https://github.com/Pondidum/Finite/blob/master/Sample.Common/States/AwaitingManagerApproval.cs
