using System;

namespace Finite.Tests.Acceptance.States
{
	public abstract class LightState : State<LightsSwitches>
	{
		public Action<StateChangeEventArgs<LightsSwitches>> OnEnterAction { get; set; }
		public Action<StateChangeEventArgs<LightsSwitches>> OnLeaveAction { get; set; }

		public override void OnEnter(object sender, StateChangeEventArgs<LightsSwitches> stateChangeArgs)
		{
			if (OnEnterAction != null)
				OnEnterAction(stateChangeArgs);
		}

		public override void OnLeave(object sender, StateChangeEventArgs<LightsSwitches> stateChangeArgs)
		{
			if (OnLeaveAction != null)
				OnLeaveAction(stateChangeArgs);
		}

	}
}
