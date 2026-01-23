using Dainiki.Components.Application.Services;
using Dainiki.Components.Database;
using Dainiki.Components.Utils;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;


namespace Dainiki
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
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();
            builder.Services.AddSingleton<JournalDatabase>(s => new JournalDatabase(DatabaseConfig.DatabasePath));
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<QuillEditorService> ();
            builder.Services.AddScoped<EntityService>();
            builder.Services.AddScoped<EntityMetricsCalculator>();


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();

#endif

            return builder.Build();
        }
    }
}
