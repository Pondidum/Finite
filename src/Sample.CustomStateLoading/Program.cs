using System;
using Finite;
using Finite.Configurations;
using Finite.StateProviders;
using Sample.CustomStateLoading.States;
using Shouldly;
using Xunit;

namespace Sample.CustomStateLoading
{
	public class Program
	{
		[Fact]
		public void When_finding_custom_states()
		{

			var scanner = new ScanningStateProvider<CreditRequest>();
			var provider = new MappingStateProvider(scanner);

			var config = new MachineConfiguration<CreditRequest>();
			config.OnStateChange(
				reset: (sender, args) => args.Switches.Progress = ((CustomState<CreditRequest>)args.Next).Type,
				enter: (sender, args) => args.Switches.Progress = ((CustomState<CreditRequest>)args.Next).Type
			);

			var request = LoadFromDatabase();

			var machine = new StateMachine<CreditRequest>(provider, request);
			machine.ResetTo(provider.StateFrom(request.Progress).GetType());

			machine.CurrentState.ShouldBeOfType<Approved>();
		}

		private CreditRequest LoadFromDatabase()
		{
			return new CreditRequest
			{
				ID = Guid.NewGuid(),
				CreatedBy =  "Andy Dote",
				CreatedOn = DateTime.Now,
				Progress = Progress.Approved
			};
		}
	}
}