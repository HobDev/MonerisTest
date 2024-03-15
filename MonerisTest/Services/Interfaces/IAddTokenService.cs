

namespace MonerisTest.Services.Interfaces
{
    public interface IAddTokenService
    {
        Task<Receipt?> SaveTokenToVault(string issuerId, string tempToken);
    }
}
