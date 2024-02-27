using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;

namespace MonerisTest
{
    [Activity(Theme = "@style/Maui.SplashTheme", 
        MainLauncher = true, 
        Exported = true,
        ConfigurationChanges = ConfigChanges.ScreenSize 
        | ConfigChanges.Orientation 
        | ConfigChanges.UiMode 
        | ConfigChanges.ScreenLayout 
        | ConfigChanges.SmallestScreenSize 
        | ConfigChanges.Density)]

    [IntentFilter(
    new string[] { Intent.ActionView },
    AutoVerify = true,
    Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
    DataScheme = "https",
    DataHost = "townmilk.com")]
    public class MainActivity : MauiAppCompatActivity
    {
    }
}
