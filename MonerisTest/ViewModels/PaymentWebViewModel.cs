

using CommunityToolkit.Mvvm.ComponentModel;
using MonerisTest.Services.Interfaces;

namespace MonerisTest.ViewModels
{
   public partial class PaymentWebViewModel: ObservableObject
    {
        [ObservableProperty]
        bool saveCard;


        private readonly IConvertTempToPermanentTokenService convertTempToPermanentTokenService;

        public PaymentWebViewModel(IConvertTempToPermanentTokenService convertTempToPermanentTokenService)
        {

            this.convertTempToPermanentTokenService = convertTempToPermanentTokenService;

            saveCard = false;
        }
    }
}
