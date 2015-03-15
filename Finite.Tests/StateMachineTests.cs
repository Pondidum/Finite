using System;
using System.CodeDom;
using System.Linq;
using System.Security.Policy;
using Finite.Tests.TestData;
using Xunit;

namespace Finite.Tests
{
	public class StateMachineTests
	{
		[Fact]
		public void When_setting_the_initial_state()
		{
			var machine = new StateMachine<TestArgs>();
			machine.InitialiseFrom(new Type[] {typeof(FirstState), typeof(SecondState), typeof(ThirdState), typeof(FourthState)});

			machine.SetStateTo<FirstState>();

			Assert.AreEqual(typeof(FirstState), machine.CurrentState);
			CollectionAssert.AreEquivalent(new Type[] {typeof (SecondState)}, machine.GetAllTargetStates());
		}

		[Fact]
		public void When_trying_to_move_to_non_allowed_state()
		{
			var machine = new StateMachine<TestArgs>();
			machine.InitialiseFrom(new Type[] { typeof(FirstState), typeof(SecondState), typeof(ThirdState), typeof(FourthState) });

			machine.SetStateTo<FirstState>();

			Assert.AreEqual(typeof(FirstState), machine.CurrentState);
			Assert.Throws<InvalidTransitionException>(() => machine.SetStateTo<ThirdState>());
		}

		[Fact]
		public void When_trying_to_move_to_an_allowed_state()
		{
			var machine = new StateMachine<TestArgs>();
			machine.InitialiseFrom(new Type[] { typeof(FirstState), typeof(SecondState), typeof(ThirdState), typeof(FourthState) });

			machine.SetStateTo<FirstState>();
			Assert.AreEqual(typeof(FirstState), machine.CurrentState);

			machine.SetStateTo<SecondState>();
			Assert.AreEqual(typeof(SecondState), machine.CurrentState);
		}
	}
}
