using System;
using System.Collections.Generic;
using System.Linq;
using Sample.Common;
using Sample.Common.States;

namespace Sample.Winforms.ManagerApplication
{
	public class ManagerPresenter : IDisposable
	{
		private readonly IManagerView _view;
		private readonly List<CreditRequest> _allRequests;

		public ManagerPresenter(IManagerView view, List<CreditRequest> allRequests)
		{
			_view = view;
			_allRequests = allRequests;

			_view.CreditRequestSelected += OnCreditRequestSelected;
			_view.ApproveClicked += OnApproveClicked;
			_view.RejectClicked += OnRejectClicked;
		}

		public void Display()
		{
			RefreshList();
			_view.ClearDisplay();
			_view.ShowDialog();
		}

		private void RefreshList()
		{
			_view.CreditRequests = _allRequests
				.Where(cr => cr.State == typeof (AwaitingManagerApproval));
		}

		private void ClearDisplay()
		{
			_view.ClearDisplay();
			_view.ApproveEnabled = false;
			_view.RejectEnabled = false;
		}

		private void OnCreditRequestSelected()
		{
			var selected = _view.SelectedRequest;

			if (selected == null)
			{
				ClearDisplay();
				return;
			}

			var machine = StateMachineBuilder.Create(selected);

			_view.ApproveEnabled = machine.CurrentState.CanTransitionTo<Approved>();
			_view.RejectEnabled = machine.CurrentState.CanTransitionTo<Rejected>();
			_view.DisplayRequest(selected);
		}


		private void OnApproveClicked()
		{
			var selected = _view.SelectedRequest;

			if (selected == null)
				return;

			var machine = StateMachineBuilder.Create(selected);

			if (machine.CurrentState.CanTransitionTo<Approved>() == false)
				return;

			machine.TransitionTo<Approved>();

			ClearDisplay();
			RefreshList();
		}

		private void OnRejectClicked()
		{
			var selected = _view.SelectedRequest;

			if (selected == null)
				return;

			var machine = StateMachineBuilder.Create(selected);

			if (machine.CurrentState.CanTransitionTo<Rejected>() == false)
				return;

			machine.TransitionTo<Rejected>();

			ClearDisplay();
			RefreshList();
		}

		public void Dispose()
		{
			_view.CreditRequestSelected -= OnCreditRequestSelected;
			_view.ApproveClicked -= OnApproveClicked;
			_view.RejectClicked -= OnRejectClicked;
		}
	}
}
