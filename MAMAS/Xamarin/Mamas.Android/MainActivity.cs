using Android.App;
using Android.Content.PM;
using Android.OS;
using Firebase;
using Prism;
using Prism.Ioc;


namespace Mamas.Droid
{
    [Activity(Label = "Mamas", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static FirebaseApp app;
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            InitFirebaseAuth(); //Inicializar el Firebase

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }

        private void InitFirebaseAuth()
        {
            var options = new FirebaseOptions.Builder()
            .SetApplicationId("1:327576449206:android:e3c3f642743376bc")
            .SetApiKey("AIzaSyBMRiOCkV_JqlvapaEhabq1KsLa0r8qhkY")
            .SetDatabaseUrl("https://mamasdam2.firebaseio.com/")
            .Build();



            if (app == null)
                app = FirebaseApp.InitializeApp(this, options, "MAMAS");

        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

