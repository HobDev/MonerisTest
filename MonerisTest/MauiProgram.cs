
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
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
                })
                .ConfigureLifecycleEvents(lifecycle =>
                {
#if IOS
					lifecycle.AddiOS(ios =>
					{
						ios.FinishedLaunching((app, data)
							=> HandleAppLink(app.UserActivity));

						ios.ContinueUserActivity((app, userActivity, handler)
							=> HandleAppLink(userActivity));

						if (OperatingSystem.IsIOSVersionAtLeast(13) || OperatingSystem.IsMacCatalystVersionAtLeast(13))
						{
							ios.SceneWillConnect((scene, sceneSession, sceneConnectionOptions)
								=> HandleAppLink(sceneConnectionOptions.UserActivities.ToArray()
									.FirstOrDefault(a => a.ActivityType == Foundation.NSUserActivityType.BrowsingWeb)));

							ios.SceneContinueUserActivity((scene, userActivity)
								=> HandleAppLink(userActivity));
						}
					});
#elif ANDROID
					lifecycle.AddAndroid(android => {
						android.OnCreate((activity, bundle) =>
						{
							var action = activity.Intent?.Action;
							var data = activity.Intent?.Data?.ToString();

							if (action == Android.Content.Intent.ActionView && data is not null)
							{
								activity.Finish();
								System.Threading.Tasks.Task.Run(() => HandleAppLink(data));
							}
						});
					});
#endif
                });

            builder.Services.AddHybridWebView();
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
            builder.Services.AddSingleton<IGetTempTokenService, GetTempTokenService>(); 
            builder.Services.AddSingleton<IConvertTempToPermanentTokenService, ConvertTempToPermanentTokenService>();
            builder.Services.AddSingleton<IPurchaseCorrectionService, PurchaseCorrectionService>();
            builder.Services.AddSingleton<IRefundService, RefundService>();
            builder.Services.AddSingleton<IIndependentRefundService, IndependentRefundService>();
            builder.Services.AddSingleton<IOpenTotalsService, OpenTotalsService>();
            builder.Services.AddSingleton<IBatchCloseService, BatchCloseService>();

            return builder;
        }

#if IOS
		static bool HandleAppLink(Foundation.NSUserActivity? userActivity)
		{
			if (userActivity is not null && userActivity.ActivityType == Foundation.NSUserActivityType.BrowsingWeb && userActivity.WebPageUrl is not null)
			{
				HandleAppLink(userActivity.WebPageUrl.ToString());
				return true;
			}
			return false;
		}
#endif

        static void HandleAppLink(string url)
        {
            if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var uri))
            {
                App.Current?.SendOnAppLinkRequestReceived(uri);
            }
        }

    }
}
