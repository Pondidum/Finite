using System;
using System.Collections.Generic;
using Sample.Common;
using Sample.Common.States;
using Sample.Winforms.NewRequestEditor;

namespace Sample.Winforms.UserApplication
{
	public class UserPresenter : IDisposable
	{
		private readonly IUserView _view;
		private readonly List<CreditRequest> _allRequests;

		public UserPresenter(IUserView view, List<CreditRequest> allRequests)
		{
			_view = view;
			_allRequests = allRequests;

			_view.CreditRequestSelected += OnCreditRequestSelected;
			_view.CreateNew += OnCreateNew;
			_view.Abandon += OnAbandon;
		}

		public void Display()
		{
			RefreshView();
			_view.ShowDialog();
		}

		private void OnCreditRequestSelected()
		{
			var selected = _view.SelectedRequest;

			if (selected == null)
			{
				_view.AbandonEnabled = false;
				return;
			}

			var currentState = StateMachineBuilder.Create(selected).CurrentState;

			_view.AbandonEnabled = currentState.CanTransitionTo<Abandoned>();
		}

		private void OnCreateNew()
		{
			using (var view = new NewRequestView())
			using (var presenter = new NewRequestPresenter(view))
			{
				var success = presenter.Display();

				if (success)
				{
					_allRequests.Add(presenter.CreditRequest);
					RefreshView();
				}
			}
		}

		private void OnAbandon()
		{
			var selected = _view.SelectedRequest;

			if (selected == null)
				return;

			var machine = StateMachineBuilder.Create(selected);

			if (machine.CurrentState.CanTransitionTo<Abandoned>() == false)
				return;

			machine.TransitionTo<Abandoned>();
			RefreshView();
		}

		private void RefreshView()
		{
			_view.CreditRequests = _allRequests;
			OnCreditRequestSelected();
		}

		public void Dispose()
		{
			_view.CreditRequestSelected -= OnCreditRequestSelected;
			_view.CreateNew -= OnCreateNew;
			_view.Abandon -= OnAbandon;
		}
	}
}
