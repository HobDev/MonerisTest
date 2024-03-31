namespace MonerisTest.Services.Implementations.Success
{
    public class SendReceiptService : ISendReceiptService
    {


        public async Task SendReceipt(RecordOfSuccessfulTransaction recordOfSuccessfulTransaction)
        {

           RecordOfSuccessfulTransaction record = new RecordOfSuccessfulTransaction(recordOfSuccessfulTransaction);

            // send receipt to customer
          //  await SendEmailReceipt(record);
        }


    }
}
