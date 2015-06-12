using System.Linq;
using System.Threading;
using Finite.StateProviders;
using Finite.Tests.Acceptance;
using Finite.Tests.Acceptance.States;
using Shouldly;
using Xunit;

namespace Finite.Tests.StateProviders
{
	public class ScanningStateProviderTests
	{
		private readonly ScanningStateProvider<LightsSwitches> _scanner;

		public ScanningStateProviderTests()
		{
			_scanner = new ScanningStateProvider<LightsSwitches>();
		}

		[Fact]
		public void The_correct_states_are_found()
		{
			var correctStates = new[]
			{
				typeof (LightOff),
				typeof (LightOnFull),
				typeof (LightOnDim)
			};

			_scanner.KnownTypes.ShouldBe(correctStates, ignoreOrder: true);
		}

		[Fact]
		public void The_scanner_will_create_instances()
		{
			_scanner
				.Execute()
				.First()
				.ShouldBeAssignableTo<State<LightsSwitches>>();
		}
	}
}
