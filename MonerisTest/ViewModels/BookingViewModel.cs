

using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MonerisTest.Services.Interfaces;

namespace MonerisTest
{
    public partial class BookingViewModel
    {
    
       

        public BookingViewModel()
        {

           
        }



        [RelayCommand]
        private async Task Purchase()
        {
          await Shell.Current.GoToAsync("PaymentWebPage");
        }

      

        


    }
}
