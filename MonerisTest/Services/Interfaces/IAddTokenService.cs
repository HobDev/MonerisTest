

namespace MonerisTest.Services.Interfaces
{
    public interface IAddTokenService
    {
        Task<string?> SaveTokenToVault(string issuerId, string tempToken);
    }
}
