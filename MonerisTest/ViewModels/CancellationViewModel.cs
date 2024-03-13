

namespace MonerisTest.ViewModels
{
    public partial class CancellationViewModel
    {

        private readonly IRefundService refundService;
        public CancellationViewModel(IRefundService refundService)
        {
            this.refundService = refundService;
        }


        [RelayCommand]
        private async Task Refund()
        {
            await refundService.Refund();
        }
    }
}
