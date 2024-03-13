

namespace MonerisTest.ViewModels
{
    public partial class CancellationViewModel
    {

        private readonly IRefundService refundService;
        public CancellationViewModel(IRefundService refundService)
        {
            try
            {
                this.refundService = refundService;
            }
            catch (Exception ex)
            {

                Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            
        }


        [RelayCommand]
        private async Task Refund()
        {
            try
            {
                await refundService.Refund();
            }
            catch (Exception ex)
            {

               await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
           
        }
    }
}
