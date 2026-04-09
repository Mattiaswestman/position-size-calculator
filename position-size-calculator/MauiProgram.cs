using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using PositionSizeCalculator.ViewModel;

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

            builder.Services.AddTransient<DetailPage>();
            builder.Services.AddTransient<DetailViewModel>();

            builder.Logging.AddDebug();

#if ANDROID
            EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
            {
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
            });
#endif
#if IOS || MACCATALYST
            EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
            {
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
            });
#endif

            return builder.Build();
        }
    }
}
