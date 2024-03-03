

namespace MonerisTest.Services.Interfaces
{
    public interface IConvertTempToPermanentTokenService
    {
        void SaveTokenToVault(string token);
    }
}
