using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using PositionSizeCalculator.Views;
using PositionSizeCalculator.ViewModels;

#if WINDOWS
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
#endif
#if ANDROID
using Android.Content.Res;
using Android.Graphics;
#endif
#if IOS || MACCATALYST
using UIKit;
#endif

namespace PositionSizeCalculator
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();

            builder.Logging.AddDebug();

            EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
            {
#if WINDOWS
                if (handler.PlatformView is TextBox textBox)
                {
                    textBox.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
                    textBox.BorderBrush = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Transparent);
                }
#endif
#if ANDROID
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
#if IOS
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
            });

            return builder.Build();
        }
    }
}
