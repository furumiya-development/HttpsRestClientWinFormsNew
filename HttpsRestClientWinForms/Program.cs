using Microsoft.Extensions.DependencyInjection;

namespace HttpsRestClientWinForms
{
    internal static class Program
    {
        /// <summary>�A�v���P�[�V�����̃��C���G���g���|�C���g</summary>
        /// <remarks>Nuget Package : Install-Package Microsoft.Extensions.DependencyInjection -Version 6.0.0</remarks>
        [STAThread]
        static void Main()
        {
            // ��DPI�ݒ��f�t�H���g�t�H���g�̐ݒ�ȂǁA�A�v���P�[�V�����\�����J�X�^�}�C�Y����ɂ́A
            // https://aka.ms/applicationconfiguration ���Q�Ƃ��Ă��������B
            ApplicationConfiguration.Initialize();
            //Application.Run(new Form1Control());
            ConfigureServices();
            Application.Run(ServiceProvider!.GetService(typeof(Form1Control)) as Form1Control);
        }

        private static IServiceProvider? ServiceProvider { get; set; }

        private static void ConfigureServices()
        {
            var services = new ServiceCollection(); //using Microsoft.Extensions.DependencyInjection���K�v
            services.AddHttpClient();
            services.AddSingleton<Form1Control>();
            ServiceProvider = services.BuildServiceProvider();
            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        }
    }
}