

namespace MonerisTest
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitMarkup()
                .ConfigureServices()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });


            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "MonerisTest.db3");
            builder.Services.AddSingleton<PaymentRepository>(s=>ActivatorUtilities.CreateInstance<PaymentRepository>(s,dbPath));

            builder.Services.AddHybridWebView();
            builder.Services.AddTransient<BookingPage>();
            builder.Services.AddTransient<BookingViewModel>();
            builder.Services.AddTransient<PaymentWebPage>();
            builder.Services.AddTransient<PaymentWebViewModel>();
            builder.Services.AddTransient<CancellationPage>();
            builder.Services.AddTransient<CancellationViewModel>();




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
            builder.Services.AddSingleton<IConvenienceFeeService, ConvenienceFeeService>();
            builder.Services.AddSingleton<IRefundService, RefundService>();


            return builder;
        }

    }
}
