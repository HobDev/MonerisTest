
using Microsoft.Extensions.Logging;
using MonerisTest.Services.Implementations;
using MonerisTest.Services.Interfaces;

namespace MonerisTest
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                  .ConfigureServices()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton <PaymentPage>();
            builder.Services.AddSingleton<PaymentViewModel>();
            builder.Services.AddTransient<PaymentWebPage>();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<ICardVerificationService, CardVerificationService>();
            builder.Services.AddSingleton<IPurchaseService, PurchaseService>();
            builder.Services.AddSingleton<IPurchaseCorrectionService, PurchaseCorrectionService>();
            builder.Services.AddSingleton<IRefundService, RefundService>();
            builder.Services.AddSingleton<IIndependentRefundService, IndependentRefundService>();
            builder.Services.AddSingleton<IOpenTotalsService, OpenTotalsService>();
            builder.Services.AddSingleton<IBatchCloseService, BatchCloseService>();

            return builder;
        }
    }
}
