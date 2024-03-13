

using SQLite;

namespace MonerisTest.DataAccessLayer
{
    public class PaymentRepository<T> where T :Entity , new()   
    {
        public SQLiteAsyncConnection? dbConnection;

        string  dbPath;

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

        public async Task<List<T>?> GetAll()
        {
            try
            {
                if(dbConnection!=null)
                {
                    return await dbConnection.Table<T>().ToListAsync();
                }
              
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                return [];
            }
            return null;
        }

        public async Task<T?> Get(int id)
        {
            try
            {
                if(dbConnection!=null)
                {
                    return await dbConnection.Table<T>().Where(i => i.Id == id).FirstOrDefaultAsync();
                }
               
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                return new T();
            }
            return null;
        }

        public async Task<int> Save(T entity)
        {
            try
            {
                if(dbConnection!=null)
                {
                    if (entity.Id != 0)
                    {
                        return await dbConnection.UpdateAsync(entity);
                    }
                    else
                    {
                        return await dbConnection.InsertAsync(entity);
                    }
                }
               
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                return 0;
            }
            return 0;
        }

        public async Task<int> Delete(T entity)
        {
            try
            {
                if(dbConnection!=null)
                {
                    return await dbConnection.DeleteAsync(entity);
                }
               
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                return 0;
            }
            return 0;
        }

        public async Task<int> DeleteAll()
        {
            try
            {
                if(dbConnection!=null)
                {
                    return await dbConnection.DeleteAllAsync<T>();
                }
                
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                return 0;
            }

            return 0;
        }



    }
}
