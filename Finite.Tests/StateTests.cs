using System;
using System.Linq;
using Shouldly;
using Xunit;

namespace Finite.Tests
{
	public class StateTests
	{
		[Fact]
		public void When_a_state_has_been_configured()
		{
			var state = new TestState();
			var states = new StateRespository<object>(new[] { state });

			states.InitialiseStates();

			Should.Throw<InvalidOperationException>(() => state.Modify());
		}

		[Fact]
		public void When_a_state_has_been_conifgured_with_no_links()
		{
			var state = new UnlinkedState();
			var states = new StateRespository<object>(new[] { state });

			states.InitialiseStates();

			Should.Throw<InvalidOperationException>(() => state.Modify());
		}

		private class TestState : State<object>
		{
			public TestState()
			{
				LinkTo<TestState>();
			}

			public void Modify()
			{
				LinkTo<TestState>();
			}
		}

		private class UnlinkedState : State<object>
		{
			public void Modify()
			{
				LinkTo<UnlinkedState>();
			}
		}
	}
}
