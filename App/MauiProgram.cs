using CommunityToolkit.Maui;
using Syncfusion.Maui.Toolkit.Hosting;

namespace App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {

            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionToolkit();

            builder.Services.AddHttpClient<DataService>(client =>
                {
                    client.BaseAddress = new Uri("https://localhost:7125/");
                });

            return builder.Build();
        }
    }
}
