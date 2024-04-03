

namespace MonerisTest.ViewModels
{
    public partial class TransactionDetailViewModel : ObservableObject, IQueryAttributable
    {

        [ObservableProperty]
        Customer? purchaser;

        [ObservableProperty]
        string? customerName;

        [ObservableProperty]
        RecordOfSuccessfulTransaction? bookingInvoice;

        Realm? realm;


        public TransactionDetailViewModel()
        {
            realm = Realm.GetInstance();
        }


        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("customerId", out object? value))
            {
                if (value is string customerId)
                {
                    Purchaser = realm?.All<Customer>().FirstOrDefault(c => c.CustomerId == customerId);
                    CustomerName = Purchaser?.Name;

                }
            }
            if (query.TryGetValue("transactionId", out object? secondValue))
            {
                if (secondValue is string transactionId)
                {
                    BookingInvoice = realm?.All<RecordOfSuccessfulTransaction>().FirstOrDefault(c => c.Id == transactionId);
                    this.OnPropertyChanged(nameof(BookingInvoice));
                }
            }
        }
    }
}
