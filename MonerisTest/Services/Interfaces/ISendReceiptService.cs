

using MonerisTest.Models;

namespace MonerisTest.Services.Interfaces
{
    public interface ISendReceiptService
    {

        Task SendReceipt(RecordOfSuccessfulTransaction recordOfSuccessfulTransaction);

    }
}
