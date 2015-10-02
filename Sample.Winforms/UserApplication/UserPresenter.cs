using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sample.Common;

namespace Sample.Winforms.UserApplication
{
	public class UserPresenter : IDisposable
	{
		private readonly IUserView _view;

		public UserPresenter(IUserView view)
		{
			_view = view;

			_view.CreditRequestSelected += OnCreditRequestSelected;
			_view.CreateNew += OnCreateNew;
			_view.Abandon += OnAbandon;
		}

		public void Display()
		{

		}

		private void OnCreditRequestSelected()
		{
			throw new NotImplementedException();
		}

		private void OnCreateNew()
		{
			throw new System.NotImplementedException();
		}

		private void OnAbandon()
		{
			throw new System.NotImplementedException();
		}

		public void Dispose()
		{
			_view.CreditRequestSelected -= OnCreditRequestSelected;
			_view.CreateNew -= OnCreateNew;
			_view.Abandon -= OnAbandon;
		}
	}

	public interface IUserView
	{
		event EventAction CreditRequestSelected;
		event EventAction CreateNew;
		event EventAction Abandon;

		IEnumerable<CreditRequest> CreditRequests { set; }

		CreditRequest SelectedRequest { get; set; }
		bool AbandonEnabled { set; }

		DialogResult ShowDialog();
		void Close();
	}
}
