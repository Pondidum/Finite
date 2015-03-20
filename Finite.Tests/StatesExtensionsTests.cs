using Finite.Tests.Acceptance;
using Finite.Tests.Acceptance.States;
using Shouldly;
using Xunit;

namespace Finite.Tests
{
	public class StatesExtensionsTests
	{
		[Fact]
		public void Scan_should_find_all_relevant_state_types()
		{
			var states = new States<LightsSwitches>().Scan();

			states.KnownTypes.ShouldBe(new[] { typeof(LightOff), typeof(LightOnFull), typeof(LightOnDim) }, true);
		}
	}
}
