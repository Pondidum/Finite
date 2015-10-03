using System;
using System.Security.Claims;
using System.Windows.Forms;
using Finite;
using Finite.StateProviders;
using Sample.Common;
using Sample.Common.States;

namespace Sample.Winforms.NewRequestEditor
{
	public class NewRequestPresenter : IDisposable
	{
		private readonly INewRequestView _view;
		private readonly CreditRequest _request;
		private readonly StateMachine<CreditRequest> _fsm;

		public NewRequestPresenter(INewRequestView view)
		{
			_view = view;
			_view.SaveRequest += OnSaveRequest;
			_view.SubmitRequest += OnSubmitRequest;
			_view.CancelRequest += OnCancelRequest;

			_request = new CreditRequest
			{
				CreatedOn = DateTime.Now,
				CreatedBy = ClaimsPrincipal.Current.Identity.Name
			};

			_fsm = new StateMachine<CreditRequest>(new ScanningStateProvider<CreditRequest>(), _request);
			_fsm.ResetTo<NewRequest>();
		}

		public bool Display()
		{
			_view.ShowDialog();

			return _view.DialogResult == DialogResult.OK;
		}



		private void OnSaveRequest()
		{
			if (_fsm.CurrentState.CanTransitionTo<NewRequest>() == false)
			{
				UpdateButtons();
				return;
			}

			_request.Amount = _view.Amount;
			_request.Justification = _view.Justification;

			_fsm.TransitionTo<NewRequest>();

			_view.DialogResult = DialogResult.OK;
			_view.Close();
		}


		private void OnSubmitRequest()
		{
			OnSaveRequest();

			if (_fsm.CurrentState.CanTransitionTo<AwaitingManagerApproval>() == false)
			{
				UpdateButtons();
				return;
			}

			_fsm.TransitionTo<AwaitingManagerApproval>();

			_view.DialogResult = DialogResult.OK;
			_view.Close();
		}

		private void OnCancelRequest()
		{
			_view.DialogResult = DialogResult.Cancel;
		}


		private void UpdateButtons()
		{
			_view.SaveEnabled = _fsm.CurrentState.CanTransitionTo<NewRequest>();
			_view.SubmitEnabled = _fsm.CurrentState.CanTransitionTo<AwaitingManagerApproval>();
		}

		public void Dispose()
		{
			_view.SaveRequest -= OnSaveRequest;
			_view.SubmitRequest -= OnSubmitRequest;
			_view.CancelRequest -= OnCancelRequest;
		}
	}
}
