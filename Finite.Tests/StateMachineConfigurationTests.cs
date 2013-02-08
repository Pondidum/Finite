using System;
using Finite.Tests.TestData;
using NUnit.Framework;

namespace Finite.Tests
{
	[TestFixture]
	public class StateMachineConfigurationTests
	{	 
		[Test]
		public void When_changing_state_onEnter_and_onLeave_should_be_called()
		{
			var machine = new StateMachine<TestArgs>();
			var enterCalled = 0;
			var leaveCalled = 0;

			machine.Configuration.OnEnterState = (prev, next) => { enterCalled++; };
			machine.Configuration.OnLeaveState = (prev, next) => { leaveCalled++; };

			machine.InitialiseFrom(new[]{ typeof(FirstState), typeof(SecondState)});
			machine.BindTo(new TestArgs());

			machine.SetStateTo<FirstState>();

			Assert.AreEqual(1, leaveCalled);
			Assert.AreEqual(1, enterCalled);
		}
	}
}
