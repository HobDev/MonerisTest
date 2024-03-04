

using MonerisTest.Services.Interfaces;

namespace MonerisTest.Services.Implementations
{
    public class ConvertTempToPermanentTokenService : IConvertTempToPermanentTokenService
    {
        public Task<string?> SaveTokenToVault(string token)
        {
            string? permanentToken=null;


            return Task.FromResult(permanentToken);
        }
    }
}
