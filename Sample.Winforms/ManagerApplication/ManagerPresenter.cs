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

		private void OnCreditRequestSelected()
		{
			var selected = _view.SelectedRequest;

			if (selected == null)
				_view.ClearDisplay();
			else
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
