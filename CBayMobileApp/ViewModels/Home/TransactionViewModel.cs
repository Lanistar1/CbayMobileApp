using CBayMobileApp.Models.Home;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace CBayMobileApp.ViewModels.Home
{
    public class TransactionViewModel : BaseViewModel
    {
        private ObservableCollection<TransactionModel> transaction;
        public ObservableCollection<TransactionModel> Transaction
        {
            get => transaction;
            set
            {
                transaction = value;
                OnPropertyChanged(nameof(Transaction));
            }
        }


        public TransactionViewModel(INavigation navigation)
        {
            Navigation = navigation;

            Transaction = new ObservableCollection<TransactionModel>{
                new TransactionModel { amount = "NGN20,000.00", description = "Withdraw to bank", time = "03:48 PM", PlusImage = "credit.png" },
                new TransactionModel { amount = "NGN20,000.00", description = "Level 10 referral bonus", time = "05:00 PM", PlusImage = "debit.png" },
                new TransactionModel { amount = "NGN250,000.00", description = "Withdraw to bank", time = "09:48 AM", PlusImage = "credit.png" },
                new TransactionModel { amount = "NGN220,000.00", description = "Level 10 referral bonus", time = "06:30 PM", PlusImage = "debit.png" },
                new TransactionModel { amount = "NGN100,000.00", description = "Withdraw to bank", time = "03:48 PM", PlusImage = "credit.png" },
          

             };
        }
    }
}
