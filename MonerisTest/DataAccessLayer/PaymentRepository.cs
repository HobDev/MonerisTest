

using SQLite;

namespace MonerisTest.DataAccessLayer
{
    public class PaymentRepository
    {
        public SQLiteAsyncConnection? dbConnection;

        string dbPath;

        const SQLite.SQLiteOpenFlags Flags=
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public PaymentRepository(string dbPath)
        {
            this.dbPath = dbPath;

            Init();

          
        }
        
        async Task Init()
        {
            try
            {
                if(dbConnection!=null)
                {
                    return;
                }
                dbConnection = new SQLiteAsyncConnection(dbPath,Flags);

                await dbConnection.CreateTableAsync<Customer>();
                await dbConnection.CreateTableAsync<CardHolderTransactionRecordPurchase>();
                await dbConnection.CreateTableAsync<CardHolderTransactionRecordConvenienceFee>();
                await dbConnection.CreateTableAsync<CardHolderTransactionRecordRefund>();   
                await dbConnection.CreateTableAsync<CardholderReceipt>();
                await dbConnection.CreateTableAsync<MerchantCopyReceipt>();
              
            }
            catch (Exception ex)
            {

               await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

    }
}
