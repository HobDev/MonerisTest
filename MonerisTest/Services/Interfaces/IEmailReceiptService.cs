

using MonerisTest.Models;

namespace MonerisTest.Services.Interfaces
{
    public interface IEmailReceiptService
    {

        Task SendReceipt(string receiptId);

    }
}
