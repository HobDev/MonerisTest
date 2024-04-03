


namespace MonerisTest.ViewModels
{
    public partial class TransactionsViewModel: ObservableObject, IQueryAttributable
    {


        [ObservableProperty]
        Customer? purchaser;

        [ObservableProperty]
        string? customerName;

        [ObservableProperty]
        ObservableCollection<RecordOfSuccessfulTransaction>? bookingInvoices;

        [ObservableProperty]
        RecordOfSuccessfulTransaction? selectedTransaction;

        Realm? realm;

        public TransactionsViewModel()
        {
            try
            {
                realm = Realm.GetInstance();
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("customerId", out object? value))
            {
                if (value is string customerId)
                {
                    Purchaser = realm?.All<Customer>().FirstOrDefault(c => c.CustomerId == customerId);
                    CustomerName = Purchaser?.Name;
                    BookingInvoices=Purchaser?.RecordOfScuccessfulTransactions.ToObservableCollection();
                }
            }
        }

        

        [RelayCommand]
        public async Task TransactionSelected()
        {
            if (SelectedTransaction != null)
            {
                Dictionary<string, object> query = new Dictionary<string, object> { { "customerId", Purchaser.CustomerId }, { "transactionId", SelectedTransaction.Id } };
                await Shell.Current.GoToAsync(nameof(TransactionDetailPage), query);
            }
        }
    }
}
