

namespace MonerisTest.Services.Interfaces
{
    public interface IConvertTempToPermanentTokenService
    {
        Task<string?> SaveTokenToVault(string token);
    }
}
