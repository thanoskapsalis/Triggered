using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Triggered
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
           
            MainPage = new NavigationPage( new MainPage());
        }

        public static MobileServiceClient client = new MobileServiceClient("https://triggered.azurewebsites.net");

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static IAuthenticate Authenticator { get; private set; }

        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
        }

        public interface IAuthenticate
        {
            Task<bool> Authenticate();
        }

    }
}
