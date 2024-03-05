﻿
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using MonerisTest.Pages;
using MonerisTest.Services.Implementations;
using MonerisTest.Services.Interfaces;
using MonerisTest.ViewModels;

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
            builder.Services.AddSingleton <BookingPage>();
            builder.Services.AddSingleton<BookingViewModel>();
            builder.Services.AddTransient<PaymentWebPage>();
            builder.Services.AddTransient<PaymentWebViewModel>();
            builder.Services.AddTransient<CancellationPage>();
            builder.Services.AddTransient<CancellationViewModel>();


        

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
        {
           
            builder.Services.AddSingleton<IPurchaseService, PurchaseService>();
            builder.Services.AddSingleton<IAddTokenService, AddTokenService>();    
            builder.Services.AddSingleton<IRefundService, RefundService>();
        

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
