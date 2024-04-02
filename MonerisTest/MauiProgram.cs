

namespace MonerisTest
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMarkup()
                .ConfigureServices()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });


           

            builder.Services.AddHybridWebView();
            builder.Services.AddTransient<CustomersPage>();
            builder.Services.AddTransient<CustomersViewModel>();
            builder.Services.AddTransient<BookingPage>();
            builder.Services.AddTransient<BookingViewModel>();
            builder.Services.AddTransient<PaymentWebPage>();
            builder.Services.AddTransient<PaymentWebViewModel>();
            builder.Services.AddTransient<ReceiptsPage>();
            builder.Services.AddTransient<ReceiptsViewModel>();
            builder.Services.AddTransient<CancellationPage>();
            builder.Services.AddTransient<CancellationViewModel>();
            builder.Services.AddTransient<FailedTransactionsPage>();
            builder.Services.AddTransient<FailedTransactionsViewModel>();
            builder.Services.AddTransient<TransactionDetailPage>();
            builder.Services.AddTransient<TransactionDetailViewModel>();
            builder.Services.AddTransient<TransactionDetailPage>();
            builder.Services.AddTransient<TransactionDetailViewModel>();




#if DEBUG

            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<ICardVerificationService, CardVerificationService>();
            builder.Services.AddSingleton<IAddTokenService, AddTokenService>();
            builder.Services.AddSingleton<IPurchaseService, PurchaseService>();
            builder.Services.AddSingleton<IRefundService, RefundService>();
            builder.Services.AddSingleton<IReceiptErrorMessageService, ReceiptErrorMessageService>();
            builder.Services.AddSingleton<ITransactionFailureService, TransactionFailureService>();
            builder.Services.AddSingleton<ITransactionSuccessService, TransactionSuccessService>();
            builder.Services.AddSingleton<IPaymentResponseHelper, PaymentResponseHelper>();


            return builder;
        }

    }
}
