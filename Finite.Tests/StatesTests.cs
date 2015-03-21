using Finite.Tests.Acceptance;
using Finite.Tests.Acceptance.States;
using Finite.Tests.TestData;
using Shouldly;
using Xunit;

namespace Finite.Tests
{
	public class StatesTests
	{
		[Fact]
		public void States_cant_be_added_if_they_dont_inherit_state_tswitches()
		{
			var states = new States<LightsSwitches>();

			Should.Throw<InvalidStateException>(() => states.Are(typeof(FirstState)));
			states.KnownTypes.ShouldBeEmpty();
		}

		[Fact]
		public void States_can_be_added_if_they_inherit_state_tswitches()
		{
			var states = new States<LightsSwitches>();

			states.Are(typeof(LightOff));

			states.KnownTypes.ShouldBe(new[] { typeof(LightOff) });
		}
	}
}
