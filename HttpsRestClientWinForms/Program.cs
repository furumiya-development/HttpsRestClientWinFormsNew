using Microsoft.Extensions.DependencyInjection;

namespace HttpsRestClientWinForms
{
    internal static class Program
    {
        /// <summary>アプリケーションのメインエントリポイント</summary>
        /// <remarks>Nuget Package : Install-Package Microsoft.Extensions.DependencyInjection -Version 6.0.0</remarks>
        [STAThread]
        static void Main()
        {
            // 高DPI設定やデフォルトフォントの設定など、アプリケーション構成をカスタマイズするには、
            // https://aka.ms/applicationconfiguration を参照してください。
            ApplicationConfiguration.Initialize();
            //Application.Run(new Form1Control());
            ConfigureServices();
            Application.Run(ServiceProvider!.GetService(typeof(Form1Control)) as Form1Control);
        }

        private static IServiceProvider? ServiceProvider { get; set; }

        private static void ConfigureServices()
        {
            var services = new ServiceCollection(); //using Microsoft.Extensions.DependencyInjectionが必要
            services.AddHttpClient();
            services.AddSingleton<Form1Control>();
            ServiceProvider = services.BuildServiceProvider();
            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        }
    }
}