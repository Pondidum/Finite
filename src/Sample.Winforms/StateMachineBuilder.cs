using Finite;
using Finite.Configurations;
using Finite.StateProviders;
using Sample.Common;

namespace Sample.Winforms
{
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
}